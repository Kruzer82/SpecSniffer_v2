@echo off&color a && Title [CAB Drivers Installer]

cd /d %~dp0

for /f "tokens=*" %%a in ('dir *.inf /b /s') do (pnputil -i -a "%%a\..\*.inf")

@echo ===================================================================================================
@echo ===================================================================================================
@echo ===================================================================================================
@echo ===================================================================================================
@echo ===================================================================================================
@echo =================================== INSTALLATION COMPLETED ========================================
@echo ===================================================================================================
@echo =========================== CHECK DEVICE MANAGER FOR MISSING DRIVERS ==============================
@echo ===================================================================================================
@echo ===================================================================================================
@echo ===================================================================================================
@echo ===================================================================================================
@echo ===================================================================================================

mmc devmgmt.msc
