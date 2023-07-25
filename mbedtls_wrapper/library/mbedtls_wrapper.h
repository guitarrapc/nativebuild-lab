#include <string.h>

#include "mbedtls/ctr_drbg.h"
#include "mbedtls/entropy.h"
#include "mbedtls/ssl.h"

struct wrapper_context
{
	mbedtls_ssl_context context; /* wrapper_context のポインターは mbedtls_ssl_context として使える */
	mbedtls_ssl_config config;
	mbedtls_x509_crt cacert;
	mbedtls_ctr_drbg_context ctr_drbg;
	mbedtls_entropy_context entropy;
} wrapper_context_t;

extern int wrapper_init(struct wrapper_context* ctx, const unsigned char* certs_buf, size_t certs_len);

extern const mbedtls_ssl_context* wrapper_get_ssl_context(const struct wrapper_context* ctx);

extern void wrapper_free(struct wrapper_context* ctx);

extern int wrapper_sizeof_wrapper_context(void);
