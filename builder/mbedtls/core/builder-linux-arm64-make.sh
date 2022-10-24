#!/bin/sh
set -eu

# install
apt update
apt install -yq --no-install-suggests --no-install-recommends make gcc libc-dev
apt install -yq --no-install-suggests --no-install-recommends python3 perl python3-pip
apt install -yq --no-install-suggests --no-install-recommends gnutls-bin doxygen graphviz
apt install -yq --no-install-suggests --no-install-recommends gcc-aarch64-linux-gnu
pip3 install jinja2

# build
cd /src
make clean
make SHARED=1 CC=aarch64-linux-gnu-gcc CFLAGS="-O2 -Werror" lib
