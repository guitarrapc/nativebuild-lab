if(NOT FIBO_VERSION_STRING)
  set(FIBO_VERSION_STRING "1.0.0")
  set(FIBO_VERSION_MAJOR "1")
endif()

cmake_minimum_required(VERSION 3.5)
project(fibo
  VERSION ${FIBO_VERSION_STRING}
  LANGUAGES C
)

set(FIBO_TOP_SOURCE_DIR "${CMAKE_CURRENT_SOURCE_DIR}/../..")
set(FIBO_LIB_SOURCE_DIR "${FIBO_TOP_SOURCE_DIR}/lib")
set(FIBO_PROG_SOURCE_DIR "${FIBO_TOP_SOURCE_DIR}/programs")

# Whether to use position independent code for the static library.  If
# we're building a shared library this is ignored and PIC is always
# used.
option(FIBO_POSITION_INDEPENDENT_LIB "Use position independent code for static library (if applicable)" ON)
option(BUILD_SHARED_LIBS "Build shared libraries" ON)
option(BUILD_STATIC_LIBS "Build static libraries" ON)
option(BUILD_PROGRAM "Build program" ON)

# sources
include_directories("${FIBO_LIB_SOURCE_DIR}")
set(FIBO_SOURCES
  "${FIBO_LIB_SOURCE_DIR}/sample.h"
  "${FIBO_LIB_SOURCE_DIR}/sample.c"
  "${FIBO_LIB_SOURCE_DIR}/fibo.c"
  "${FIBO_LIB_SOURCE_DIR}/fibo.h")
set(FIBO_CLI_SOURCES
  "${FIBO_PROG_SOURCE_DIR}/fibocli.c")

if(BUILD_STATIC_LIBS)
  set(FIBO_LINK_LIBRARY fibo_static)
else()
  list(APPEND FIBO_CLI_SOURCES ${FIBO_SOURCES})
endif()

# libfibo.so,.dll,.dylib
if (NOT FIBO_LIB_NAME)
  set(FIBO_LIB_NAME fibo)
endif()
if(BUILD_SHARED_LIBS)
  add_library(fibo_shared SHARED ${FIBO_SOURCES})
  set_target_properties(fibo_shared PROPERTIES
  OUTPUT_NAME ${FIBO_LIB_NAME}
  SOVERSION "${FIBO_VERSION_MAJOR}"
  VERSION "${FIBO_VERSION_STRING}")
  # export method symbol for Windows .dll. It enable C# to dllimport signature.
  if(MSVC)
    target_compile_definitions(fibo_shared PRIVATE DLL_EXPORT=1)
  endif()
endif()

# libfibo.a,.lib
if(BUILD_STATIC_LIBS)
  add_library(fibo_static STATIC ${FIBO_SOURCES})
  set_target_properties(fibo_static PROPERTIES
  OUTPUT_NAME ${FIBO_LIB_NAME}
  POSITION_INDEPENDENT_CODE ${FIBO_POSITION_INDEPENDENT_LIB})
endif()

# fibocli, .exe
if(BUILD_PROGRAM)
  add_executable(fibocli ${FIBO_CLI_SOURCES})
  set_target_properties(fibocli PROPERTIES OUTPUT_NAME fibocli)
  if (BUILD_STATIC_LIBS)
    target_link_libraries(fibocli ${FIBO_LINK_LIBRARY})
  endif()
endif()
