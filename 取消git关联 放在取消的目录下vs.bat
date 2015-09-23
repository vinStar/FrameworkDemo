@echo On @Rem É¾³ýgit°æ±¾¿ØÖÆÄ¿Â¼ @PROMPT [Com] 
 
@for /r . %%a in (.) do @if exist "%%a\.vs" rd /s /q "%%a\.vs" 
@Rem for /r . %%a in (.) do @if exist "%%a\.vs" 
@echo "%%a\.vs" 
 
@echo Mission Completed. @pause