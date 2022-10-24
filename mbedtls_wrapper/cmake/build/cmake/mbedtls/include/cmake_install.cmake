# Install script for directory: C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include

# Set the install prefix
if(NOT DEFINED CMAKE_INSTALL_PREFIX)
  set(CMAKE_INSTALL_PREFIX "C:/Program Files/wrapper")
endif()
string(REGEX REPLACE "/$" "" CMAKE_INSTALL_PREFIX "${CMAKE_INSTALL_PREFIX}")

# Set the install configuration name.
if(NOT DEFINED CMAKE_INSTALL_CONFIG_NAME)
  if(BUILD_TYPE)
    string(REGEX REPLACE "^[^A-Za-z0-9_]+" ""
           CMAKE_INSTALL_CONFIG_NAME "${BUILD_TYPE}")
  else()
    set(CMAKE_INSTALL_CONFIG_NAME "Release")
  endif()
  message(STATUS "Install configuration: \"${CMAKE_INSTALL_CONFIG_NAME}\"")
endif()

# Set the component getting installed.
if(NOT CMAKE_INSTALL_COMPONENT)
  if(COMPONENT)
    message(STATUS "Install component: \"${COMPONENT}\"")
    set(CMAKE_INSTALL_COMPONENT "${COMPONENT}")
  else()
    set(CMAKE_INSTALL_COMPONENT)
  endif()
endif()

# Is this installation the result of a crosscompile?
if(NOT DEFINED CMAKE_CROSSCOMPILING)
  set(CMAKE_CROSSCOMPILING "FALSE")
endif()

if(CMAKE_INSTALL_COMPONENT STREQUAL "Unspecified" OR NOT CMAKE_INSTALL_COMPONENT)
  file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/include/mbedtls" TYPE FILE PERMISSIONS OWNER_READ OWNER_WRITE GROUP_READ WORLD_READ FILES
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/aes.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/aria.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/asn1.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/asn1write.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/base64.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/bignum.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/build_info.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/camellia.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/ccm.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/chacha20.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/chachapoly.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/check_config.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/cipher.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/cmac.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/compat-2.x.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/config_psa.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/constant_time.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/ctr_drbg.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/debug.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/des.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/dhm.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/ecdh.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/ecdsa.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/ecjpake.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/ecp.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/entropy.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/error.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/gcm.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/hkdf.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/hmac_drbg.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/mbedtls_config.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/md.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/md5.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/memory_buffer_alloc.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/net_sockets.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/nist_kw.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/oid.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/pem.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/pk.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/pkcs12.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/pkcs5.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/platform.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/platform_time.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/platform_util.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/poly1305.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/private_access.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/psa_util.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/ripemd160.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/rsa.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/sha1.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/sha256.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/sha512.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/ssl.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/ssl_cache.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/ssl_ciphersuites.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/ssl_cookie.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/ssl_ticket.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/threading.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/timing.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/version.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/x509.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/x509_crl.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/x509_crt.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/mbedtls/x509_csr.h"
    )
endif()

if(CMAKE_INSTALL_COMPONENT STREQUAL "Unspecified" OR NOT CMAKE_INSTALL_COMPONENT)
  file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/include/psa" TYPE FILE PERMISSIONS OWNER_READ OWNER_WRITE GROUP_READ WORLD_READ FILES
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/psa/crypto.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/psa/crypto_builtin_composites.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/psa/crypto_builtin_primitives.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/psa/crypto_compat.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/psa/crypto_config.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/psa/crypto_driver_common.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/psa/crypto_driver_contexts_composites.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/psa/crypto_driver_contexts_primitives.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/psa/crypto_extra.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/psa/crypto_platform.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/psa/crypto_se_driver.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/psa/crypto_sizes.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/psa/crypto_struct.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/psa/crypto_types.h"
    "C:/git/guitarrapc/nativebuild-lab/mbedtls_wrapper/cmake/mbedtls/include/psa/crypto_values.h"
    )
endif()

