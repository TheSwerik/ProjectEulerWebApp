@echo off
pg_ctl -D "C:\Program Files\PostgreSQL\12\data" start
pg_ctl -D "C:\Program Files\PostgreSQL\12\data" status
set /p DUMMY=Hit ENTER to stop...