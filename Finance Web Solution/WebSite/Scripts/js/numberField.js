(function($){
	$.fn.numberField = function(opts){
		return $(this).each(function(){
			if(this.tagName.toLowerCase() != 'input'){return ;}

			if( typeof(opts) == 'string' ){
				var instance = $(this).data('_numberField');
				if( !instance ){return ;}
				var args = Array.prototype.slice.call( arguments, 1 );
				if( typeof(instance[opts]) === 'function' ){
					instance[opts].apply(instance, args);
				}
				
			}

			else {
				var instance = $(this).data('_numberField');
				if( instance ){return ;}
				instance = new $.NumberField($(this),opts);
				$(this).data('_numberField', instance);
			}
		});
	}


	$.NumberField = function(obj, opts){
		this.input = obj;
		this.opts = $.extend(true, {}, $.NumberField.defaults, opts);
		this._init();
	}

	$.fn.getNumberField = function(){
		return $.NumberField.getNumberField(this);
	}


	$.NumberField.getNumberField = function(obj){
		obj = $(obj)
		if(obj.length == 0) {
			return ;
		} else if (obj.length == 1){
			return obj.data('_numberField');
		} else if ( obj.length > 1) {
			var array = [];
			obj.each(function(idx){
				array.push(this.data('_numberField'));
			})
			return array;
		}
	}

	$.NumberField.prototype = {

		constructor: $.NumberField,


		_init: function(){
			var opts = this.opts, min = parseFloat(opts.min), max = parseFloat(opts.max), 
				step = parseFloat(opts.step), precision = parseInt(opts.precision);
			this.min = !isNaN(min) ? min : Number.NEGATIVE_INFINITY;
			this.max = !isNaN(max) ? max : Number.MAX_VALUE;
			this.step = !isNaN(step) ? step : 1;
			this.precision = isNaN(precision) || !opts.decimal || precision < 0 ? 0 : precision;
			this.allowedReg = this._getAllowedReg();
			this.input.css('ime-mode', 'disabled');

			this._initVal();
			this._initDisabled();
			this._bindEvent();
		},


		_getAllowedReg: function(){
			var opts = this.opts, allowed = '0123456789', reg;
			if(opts.decimal){
				allowed += '.';
			}
			if(this.min < 0){
				allowed += '-';
			}
			allowed = allowed.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, '\\$&');
			reg = new RegExp('[' + allowed + ']');
			return reg;
		},


		_initVal: function(){
			var val = this._getProcessedVal(this.opts.value);
			if(val === false){
				val = this._getProcessedVal(this.input.val());
				if(val === false){
					val = '';
				}
			}
			this._val = this.originVal = val;
			this.input.val(val);
		},


		_initDisabled: function(){
			var opts = this.opts;
			this._disabled = opts.disabled === true ? true : opts.disabled === false ? false : !!this.input.attr('disabled');
			this.originDisabled = this._disabled;
			this._handleDisabled(this._disabled);
		},


		_bindEvent: function(){
			var self = this, opts = self.opts;
			var KEYS = {
				'up': 38,
				'down': 40
			}

			var mouseWheel = 'mousewheel';

			this.input.on('keydown', function(e){
				var which = e.which;
				if( which == KEYS.up || which == KEYS.down ){
					if( !opts.keyEnable ){ return ;}
					var operator = which == KEYS.up ? 'plus' : 'minus';
					self._handleAdjusting(operator);
					e.preventDefault();
				}
			})
			.on('keypress', function(e){
				var charCode = typeof e.charCode != 'undefined' ? e.charCode : e.keyCode; 
				var keyChar = $.trim(String.fromCharCode(charCode));
				if(charCode != 0 && !self.allowedReg.test(keyChar)){
					e.preventDefault();
				}
			})
			.on('keyup', function(e){
				self._clearAutoRepeat();
			})
			.on('focus', function(e){
				self.focus = true;
				//self.wrap.addClass(opts.activeCls);
			})
			.on('blur', function(e){
				self.focus = false;
				//self.wrap.removeClass(opts.activeCls);
				var val = $.trim(self.input.val());
				//alert('blur')
				//self._clearAutoRepeat();
				if(val === self._val){return ;}
				if(!self.setValue(val)){
					self.input.val(self._val);
				}
			})
			.on(mouseWheel,function(e){
				e.preventDefault();
				if (!self.focus || !opts.wheelEnable) {return ;}
				e = e.originalEvent;
				var delta = e.wheelDelta ? (e.wheelDelta / 120) : (- e.detail / 3);
				var operator = delta == 1 ? 'plus' : 'minus';
				var val = self.input.val();
				if(val !== self._val && !self.setValue(val)){
					self.input.val(this._val);
				}
				self._adjustVal(operator);
			});
		},


		/**
			*瀵瑰師濮嬪€艰繘琛屽鐞�
			*@return {false || string || number} 鍘熷鍊间负绌鸿繑鍥�''锛屼负NaN杩斿洖false锛屼负number杩斿洖澶勭悊濂界殑鏁板瓧
		*/
		_getProcessedVal: function(val){
			if(typeof val == 'string' && $.trim(val) === '') {return '';}
			if(typeof val == 'string' && $.trim(val) === '鑷姩') {return val;}
			val = parseFloat(val);
			if(isNaN(val)){return false;}
			val = val > this.max ? this.max : val < this.min ? this.min : val;
			val = val.toFixed(this.precision);
			if (!this.opts.forceDecimal) {
				val = parseFloat(val);
			}
			return val;
		},


		enable: function(){
			this._handleDisabled(false);
		},


		disable: function(){
			this._handleDisabled(true);
		},


		_handleDisabled: function(disabled){
			var opts = this.opts;
			disabled === true ? this.input.addClass(opts.inputDisabledCls) : this.input.removeClass(opts.inputDisabledCls);
			this._disabled = disabled;
			this.input.attr('disabled', disabled);
		},

		
		/**
			*鏈夊井璋冨彂鐢熸椂锛屽寰皟杩涜澶勭悊
		*/
		_handleAdjusting: function(operator){
			var val = this.input.val();
			if(val !== this._val && !this.setValue(val)){
				this.input.val(this._val);
			}

			//宸茬粡鍒拌揪鏈€澶у€硷紝鏈€灏忓€兼椂
			if( (this._val === this.max && operator == 'plus') || (this._val === this.min && operator == 'minus') ){
				return ;
			}

			if(this.opts.autoRepeat){
				this._clearAutoRepeat();
				this._setAutoRepeat(operator);
			}
			this._adjustVal(operator);
		},


		/**
			*寰皟鍊�
		*/
		_adjustVal: function(operator){
			//宸茬粡鍒拌揪鏈€澶у€硷紝鏈€灏忓€兼椂
			if( (this._val === this.max && operator == 'plus') || (this._val === this.min && operator == 'minus') ){
				this._clearAutoRepeat();
				return ;
			}
			var baseVal = this._val !== '' ? this._val : this.min < 0 && this.min > Number.NEGATIVE_INFINITY ? this.min : 0;
			var val = operator == 'plus' ? baseVal + this.step : baseVal - this.step;
			this.setValue(val);
		},


		_setAutoRepeat: function(operator){
			var opts = this.opts, self = this;
			self.autoTimer = window.setTimeout(function(){
				self.autoRepeater = window.setInterval(function(){
					self._adjustVal(operator);
				}, opts.interval);
			},opts.delay);
		},


		_clearAutoRepeat: function(){
			if(this.autoTimer){
				window.clearTimeout(this.autoTimer);
			}
			if(this.autoRepeater){
				window.clearTimeout(this.autoRepeater);
			}
		},


		setValue: function(val){
			var opts = this.opts;
			if(this._disabled){return ;}
			val = this._getProcessedVal(val);
			if(val === false){return ;}
			this.input.val(val);
			this._val = val;
			return true;
		}

	}


	$.NumberField.defaults = {
		value: undefined,
		max: undefined,
		min: undefined,
		step: 1,
		decimal: false,
		precision: 2,
		disabled: undefined,
		keyEnable: false,
		wheelEnable: false,
		autoRepeat: true,
		delay: 400, 
		interval: 80,

		inputCls: 'ui-input',
		inputDisabledCls: 'ui-input-disabled'
		
	}


})(jQuery);