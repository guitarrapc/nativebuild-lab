if(NOT WRAPPER_VERSION_STRING)
  set(WRAPPER_VERSION_STRING "1.0.0")
  set(WRAPPER_VERSION_MAJOR "1")
endif()

cmake_minimum_required(VERSION 3.5)
project(wrapper
  VERSION ${WRAPPER_VERSION_STRING}
  LANGUAGES C
)

set(WRAPPER_TOP_SOURCE_DIR "${CMAKE_CURRENT_SOURCE_DIR}/..")
set(WRAPPER_LIB_SOURCE_DIR "${WRAPPER_TOP_SOURCE_DIR}/lib")
set(WRAPPER_PROG_SOURCE_DIR "${WRAPPER_TOP_SOURCE_DIR}/programs")

# Whether to use position independent code for the static library.  If
# we're building a shared library this is ignored and PIC is always
# used.
option(WRAPPER_POSITION_INDEPENDENT_LIB "Use position independent code for static library (if applicable)" ON)
option(BUILD_SHARED_LIBS "Build shared libraries" ON)
option(BUILD_STATIC_LIBS "Build static libraries" ON)
option(BUILD_PROGRAM "Build program" ON)

# sources
include_directories("${WRAPPER_LIB_SOURCE_DIR}")
set(WRAPPER_SOURCES
  "${WRAPPER_LIB_SOURCE_DIR}/fibo.h"
  "${WRAPPER_LIB_SOURCE_DIR}/wrapper.h"
  "${WRAPPER_LIB_SOURCE_DIR}/wrapper.c")
set(WRAPPER_CLI_SOURCES
  "${WRAPPER_PROG_SOURCE_DIR}/wrappercli.c")
if(MSVC)
  set(FIBO_LIBS "${WRAPPER_LIB_SOURCE_DIR}/fiboedit.lib")
else()
  set(FIBO_LIBS "${WRAPPER_LIB_SOURCE_DIR}/libfiboedit.a")
endif()

if(BUILD_STATIC_LIBS)
  set(WRAPPER_LINK_LIBRARY wrapper_static)
else()
  list(APPEND WRAPPER_CLI_SOURCES ${WRAPPER_SOURCES})
endif()

# add libfiboedit.a
add_library(fiboedit STATIC IMPORTED)
set_target_properties(fiboedit PROPERTIES IMPORTED_LOCATION ${FIBO_LIBS})

# libwrapper.so,.dll,.dylib
if (NOT WRAPPER_LIB_NAME)
  set(WRAPPER_LIB_NAME wrapper)
endif()
if(BUILD_SHARED_LIBS)
  add_library(wrapper_shared SHARED ${WRAPPER_SOURCES})
  target_link_libraries(wrapper_shared fiboedit)

  set_target_properties(wrapper_shared PROPERTIES
  OUTPUT_NAME ${WRAPPER_LIB_NAME}
  SOVERSION "${WRAPPER_VERSION_MAJOR}"
  VERSION "${WRAPPER_VERSION_STRING}")
  # export method symbol for Windows .dll. It enable C# to dllimport signature.
  if(MSVC)
    target_compile_definitions(wrapper_shared PRIVATE DLL_EXPORT=1)
  endif()
endif()

# libwrapper.a,.lib
if(BUILD_STATIC_LIBS)
  add_library(wrapper_static STATIC ${WRAPPER_SOURCES})
  target_link_libraries(wrapper_static fiboedit)

  set_target_properties(wrapper_static PROPERTIES
  OUTPUT_NAME ${WRAPPER_LIB_NAME}
  POSITION_INDEPENDENT_CODE ${WRAPPER_POSITION_INDEPENDENT_LIB})
endif()

# wrappercli, .exe
if(BUILD_PROGRAM)
  add_executable(wrappercli ${WRAPPER_CLI_SOURCES})
  set_target_properties(wrappercli PROPERTIES OUTPUT_NAME wrappercli)
  if (BUILD_STATIC_LIBS)
    target_link_libraries(wrappercli ${WRAPPER_LINK_LIBRARY})
  endif()
endif()
