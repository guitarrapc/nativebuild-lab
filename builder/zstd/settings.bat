set SRC_DIR=zstd
set MAKE_LIB=%SRC_DIR%\lib
set MAKE_PROGRAMS=%SRC_DIR%\programs
set CMAKE_LIB=%SRC_DIR%\build\cmake\build\lib
set CMAKE_PROGRAM=%SRC_DIR%\build\cmake\build\programs

set LIBNAME=libzstd
set EXENAME=zstd

cd %SRC_DIR%
  FOR /F "tokens=* USEBACKQ" %%F IN (`git tag --points-at HEAD -l -n0 ^| findstr "^v"`) DO (SET VERSION=%%F)
  set GIT_VERSION=%VERSION%
  :: 'v1.5.2 ' -> '1.5.2'
  set FILE_VERSION=%VERSION:~1%
cd ..
