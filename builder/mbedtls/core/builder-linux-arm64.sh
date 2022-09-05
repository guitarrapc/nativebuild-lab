#!/bin/sh
set -eu

# install
apt-get update
apt-get install -yq --no-install-suggests --no-install-recommends make gcc libc-dev
apt-get install -yq --no-install-suggests --no-install-recommends python3 perl python3-pip
apt-get install -yq --no-install-suggests --no-install-recommends gnutls-bin doxygen graphviz
apt-get install -yq --no-install-suggests --no-install-recommends gcc-arm-none-eabi libnewlib-arm-none-eabi gcc-arm-linux-gnueabi libc6-dev-armel-cross

pip3 install jinja2

# build
# require '-fPIC'. cipher.o: relocation R_X86_64_PC32 against symbol `mbedtls_cipher_supported' can not be used when making a shared object; recompile with -fPIC
cd /src
make clean
make SHARED=1 CFLAGS="-O2 -Werror -fPIC" lib
