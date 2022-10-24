if(NOT WRAPPER_VERSION_STRING)
  set(WRAPPER_VERSION_STRING "3.2.1")
  set(WRAPPER_VERSION_MAJOR "3")
endif()

cmake_minimum_required(VERSION 3.5)
project(wrapper
  VERSION ${WRAPPER_VERSION_STRING}
  LANGUAGES C
)

set(WRAPPER_TOP_SOURCE_DIR "${CMAKE_CURRENT_SOURCE_DIR}")
set(WRAPPER_LIB_SOURCE_DIR "${WRAPPER_TOP_SOURCE_DIR}/library")

set(MBEDTLS_TOP_DIR "${CMAKE_CURRENT_SOURCE_DIR}/cmake/mbedtls")
set(MBEDTLS_LIB_SOURCE_DIR "${MBEDTLS_TOP_DIR}/library")
set(MBEDTLS_INCLUDE_MBEDTLS_SOURCE_DIR "${MBEDTLS_TOP_DIR}/include/mbedtls")
set(MBEDTLS_INCLUDE_PSA_SOURCE_DIR "${MBEDTLS_TOP_DIR}/include/psa")

# Whether to use position independent code for the static library.  If
# we're building a shared library this is ignored and PIC is always
# used.
option(WRAPPER_POSITION_INDEPENDENT_LIB "Use position independent code for static library (if applicable)" ON)
option(BUILD_SHARED_LIBS "Build shared libraries" ON)
option(BUILD_STATIC_LIBS "Build static libraries" ON)
option(ENABLE_TESTING "Build mbed TLS tests." OFF)

add_subdirectory("${MBEDTLS_TOP_DIR}/include")

# sources
include_directories("${WRAPPER_LIB_SOURCE_DIR}")
include_directories("${MBEDTLS_LIB_SOURCE_DIR}")
include_directories("${MBEDTLS_INCLUDE_MBEDTLS_SOURCE_DIR}")
include_directories("${MBEDTLS_INCLUDE_PSA_SOURCE_DIR}")
set(WRAPPER_SOURCES
  "${WRAPPER_LIB_SOURCE_DIR}/mbedtls_wrapper.c")
if(MSVC)
  set(WRAPPER_LIBS "${MBEDTLS_TOP_DIR}/mbedtls_wrapper.lib")
else()
  set(WRAPPER_LIBS "${MBEDTLS_TOP_DIR}/libmbedtls_wrapper.a")
endif()

if(BUILD_STATIC_LIBS)
  set(WRAPPER_LINK_LIBRARY wrapper_static)
endif()

# add libxmbedcrypto.a
# add libmbedx509.a
# add libmbedtls.a
add_library(mbedcrypto STATIC IMPORTED)
set_target_properties(mbedcrypto PROPERTIES IMPORTED_LOCATION ${WRAPPER_LIBS})
add_library(mbedx509 STATIC IMPORTED)
set_target_properties(mbedx509 PROPERTIES IMPORTED_LOCATION ${WRAPPER_LIBS})
add_library(mbedtls STATIC IMPORTED)
set_target_properties(mbedtls PROPERTIES IMPORTED_LOCATION ${WRAPPER_LIBS})

# libwrapper.so,.dll,.dylib
if (NOT WRAPPER_LIB_NAME)
  set(WRAPPER_LIB_NAME wrapper)
endif()
if(BUILD_SHARED_LIBS)
  add_library(wrapper_shared SHARED ${WRAPPER_SOURCES})
  target_link_libraries(wrapper_shared mbedcrypto mbedx509 mbedtls)

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
  target_link_libraries(wrapper_static mbedcrypto mbedx509 mbedtls)

  set_target_properties(wrapper_static PROPERTIES
  OUTPUT_NAME ${WRAPPER_LIB_NAME}
  POSITION_INDEPENDENT_CODE ${WRAPPER_POSITION_INDEPENDENT_LIB})
endif()
