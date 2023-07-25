# references: see https://docs.oracle.com/cd/E19504-01/802-5880/6i9k05dhg/index.html
# COMPILE.c: make's predefined Macro. same as `COMPILE.c=$(CC) $(CFLAGS) $(CPPFLAGS) -c`
# wildcard *.c: get all *.c files in directory

CC ?= gcc # use `gcc`, instead of `cc`

.PHONY: example
example: hello

.PHONY: hello
hello: hello.c
	$(CC) hello.c -o hello

.PHONY: clean
clean:
	$(RM) hello

# simple sample
# make && ./hello
# hello: hello.c
#	gcc hello.c -o hello
#	gcc hello.c -o $@ # same
