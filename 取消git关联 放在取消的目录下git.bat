@echo On @Rem ɾ��git�汾����Ŀ¼ @PROMPT [Com] 
 
@for /r . %%a in (.) do @if exist "%%a\.git" rd /s /q "%%a\.git" 
@Rem for /r . %%a in (.) do @if exist "%%a\.git" 
@echo "%%a\.git" 
 
@echo Mission Completed. @pause