# notes:
# You can override default settings. default settings can show with `make -p`
# if `CC` not exists, build with cc.
# if `CFLAGS` not exists, no compilation option.

# references:
# -C:
# -O3: Optimization level 3
# -Werror: treat warning as error
# -Wall: add compiler check
# -Wextra: additional compiler check. Wall is not all.
# -o FOO: output binary
# $@: target file name.

# operators: see https://ftp.gnu.org/old-gnu/Manuals/make-3.79.1/html_chapter/make_6.html#SEC59
# := : Simply expanded variables. The value of a simply expanded variable is scanned once and for all, expanding any references to other variables and functions, when the variable is defined.
# = : recursively expanded variable. if it contains references to other variables, these references are expanded whenever this variable is substituted (in the course of expanding some other string).
# ?= : conditional variable assignment operator, because it only has an effect if the variable is not yet defined.


LIBDIR = lib
PRGDIR = programs
EXPDIR = examples

.PHONY: default
default: lib

.PHONY: all
all: lib program

.PHONY: lib
lib:
	$(MAKE) -C $(LIBDIR) $@

.PHONY: program
program:
	$(MAKE) -C $(PRGDIR) $@

.PHONY: clean
clean:
	$(MAKE) -C $(LIBDIR) $@
	$(MAKE) -C $(PRGDIR) $@
	$(MAKE) -C $(EXPDIR) $@
	@echo Cleaning completed
