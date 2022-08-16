# Install script for directory: /Users/guitarrapc/github/guitarrapc/nativebuild-lab/zstd/build/cmake/programs

# Set the install prefix
if(NOT DEFINED CMAKE_INSTALL_PREFIX)
  set(CMAKE_INSTALL_PREFIX "/Users/guitarrapc/github/guitarrapc/nativebuild-lab/builder/zstd_ios/_install")
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
  set(CMAKE_CROSSCOMPILING "TRUE")
endif()

# Set default install directory permissions.
if(NOT DEFINED CMAKE_OBJDUMP)
  set(CMAKE_OBJDUMP "/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/bin/objdump")
endif()

set(CMAKE_BINARY_DIR "/Users/guitarrapc/github/guitarrapc/nativebuild-lab/builder/zstd_ios/_builds")

if(NOT PLATFORM_NAME)
  if(NOT "$ENV{PLATFORM_NAME}" STREQUAL "")
    set(PLATFORM_NAME "$ENV{PLATFORM_NAME}")
  endif()
  if(NOT PLATFORM_NAME)
    set(PLATFORM_NAME iphoneos)
  endif()
endif()

if(NOT EFFECTIVE_PLATFORM_NAME)
  if(NOT "$ENV{EFFECTIVE_PLATFORM_NAME}" STREQUAL "")
    set(EFFECTIVE_PLATFORM_NAME "$ENV{EFFECTIVE_PLATFORM_NAME}")
  endif()
  if(NOT EFFECTIVE_PLATFORM_NAME)
    set(EFFECTIVE_PLATFORM_NAME -iphoneos)
  endif()
endif()

if(CMAKE_INSTALL_COMPONENT STREQUAL "Unspecified" OR NOT CMAKE_INSTALL_COMPONENT)
  if(CMAKE_INSTALL_CONFIG_NAME MATCHES "^([Dd][Ee][Bb][Uu][Gg])$")
    file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/bin" TYPE DIRECTORY FILES "/Users/guitarrapc/github/guitarrapc/nativebuild-lab/builder/zstd_ios/_builds/programs/Debug${EFFECTIVE_PLATFORM_NAME}/zstd.app" USE_SOURCE_PERMISSIONS)
    if(EXISTS "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/bin/zstd.app/zstd" AND
       NOT IS_SYMLINK "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/bin/zstd.app/zstd")
      include(CMakeIOSInstallCombined)
      ios_install_combined("zstd" "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/bin/zstd.app/zstd")
    endif()
  elseif(CMAKE_INSTALL_CONFIG_NAME MATCHES "^([Rr][Ee][Ll][Ee][Aa][Ss][Ee])$")
    file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/bin" TYPE DIRECTORY FILES "/Users/guitarrapc/github/guitarrapc/nativebuild-lab/builder/zstd_ios/_builds/programs/Release${EFFECTIVE_PLATFORM_NAME}/zstd.app" USE_SOURCE_PERMISSIONS)
    if(EXISTS "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/bin/zstd.app/zstd" AND
       NOT IS_SYMLINK "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/bin/zstd.app/zstd")
      include(CMakeIOSInstallCombined)
      ios_install_combined("zstd" "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/bin/zstd.app/zstd")
    endif()
  elseif(CMAKE_INSTALL_CONFIG_NAME MATCHES "^([Mm][Ii][Nn][Ss][Ii][Zz][Ee][Rr][Ee][Ll])$")
    file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/bin" TYPE DIRECTORY FILES "/Users/guitarrapc/github/guitarrapc/nativebuild-lab/builder/zstd_ios/_builds/programs/MinSizeRel${EFFECTIVE_PLATFORM_NAME}/zstd.app" USE_SOURCE_PERMISSIONS)
    if(EXISTS "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/bin/zstd.app/zstd" AND
       NOT IS_SYMLINK "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/bin/zstd.app/zstd")
      include(CMakeIOSInstallCombined)
      ios_install_combined("zstd" "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/bin/zstd.app/zstd")
    endif()
  elseif(CMAKE_INSTALL_CONFIG_NAME MATCHES "^([Rr][Ee][Ll][Ww][Ii][Tt][Hh][Dd][Ee][Bb][Ii][Nn][Ff][Oo])$")
    file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/bin" TYPE DIRECTORY FILES "/Users/guitarrapc/github/guitarrapc/nativebuild-lab/builder/zstd_ios/_builds/programs/RelWithDebInfo${EFFECTIVE_PLATFORM_NAME}/zstd.app" USE_SOURCE_PERMISSIONS)
    if(EXISTS "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/bin/zstd.app/zstd" AND
       NOT IS_SYMLINK "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/bin/zstd.app/zstd")
      include(CMakeIOSInstallCombined)
      ios_install_combined("zstd" "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/bin/zstd.app/zstd")
    endif()
  endif()
endif()

if(CMAKE_INSTALL_COMPONENT STREQUAL "Unspecified" OR NOT CMAKE_INSTALL_COMPONENT)
  file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/bin" TYPE FILE FILES "/Users/guitarrapc/github/guitarrapc/nativebuild-lab/builder/zstd_ios/_builds/programs/zstdcat")
endif()

if(CMAKE_INSTALL_COMPONENT STREQUAL "Unspecified" OR NOT CMAKE_INSTALL_COMPONENT)
  file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/bin" TYPE FILE FILES "/Users/guitarrapc/github/guitarrapc/nativebuild-lab/builder/zstd_ios/_builds/programs/unzstd")
endif()

if(CMAKE_INSTALL_COMPONENT STREQUAL "Unspecified" OR NOT CMAKE_INSTALL_COMPONENT)
  file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/bin" TYPE PROGRAM FILES "/Users/guitarrapc/github/guitarrapc/nativebuild-lab/zstd/build/cmake/../../programs/zstdgrep")
endif()

if(CMAKE_INSTALL_COMPONENT STREQUAL "Unspecified" OR NOT CMAKE_INSTALL_COMPONENT)
  file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/bin" TYPE PROGRAM FILES "/Users/guitarrapc/github/guitarrapc/nativebuild-lab/zstd/build/cmake/../../programs/zstdless")
endif()

if(CMAKE_INSTALL_COMPONENT STREQUAL "Unspecified" OR NOT CMAKE_INSTALL_COMPONENT)
  file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/share/man/man1" TYPE FILE FILES
    "/Users/guitarrapc/github/guitarrapc/nativebuild-lab/builder/zstd_ios/_builds/programs/zstd.1"
    "/Users/guitarrapc/github/guitarrapc/nativebuild-lab/builder/zstd_ios/_builds/programs/zstdcat.1"
    "/Users/guitarrapc/github/guitarrapc/nativebuild-lab/builder/zstd_ios/_builds/programs/unzstd.1"
    "/Users/guitarrapc/github/guitarrapc/nativebuild-lab/builder/zstd_ios/_builds/programs/zstdgrep.1"
    "/Users/guitarrapc/github/guitarrapc/nativebuild-lab/builder/zstd_ios/_builds/programs/zstdless.1"
    )
endif()

if(CMAKE_INSTALL_COMPONENT STREQUAL "Unspecified" OR NOT CMAKE_INSTALL_COMPONENT)
  file(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/bin" TYPE FILE FILES "/Users/guitarrapc/github/guitarrapc/nativebuild-lab/builder/zstd_ios/_builds/programs/zstdmt")
endif()

