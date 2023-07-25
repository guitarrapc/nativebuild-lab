#include <string.h>

#include "mbedtls/ctr_drbg.h"
#include "mbedtls/entropy.h"
#include "mbedtls/ssl.h"
#include "mbedtls_wrapper.h"

int wrapper_init(struct wrapper_context* ctx, const unsigned char* certs_buf, size_t certs_len)
{
	mbedtls_ssl_init(&ctx->context);
	mbedtls_ssl_config_init(&ctx->config);
	mbedtls_entropy_init(&ctx->entropy);
	mbedtls_ctr_drbg_init(&ctx->ctr_drbg);
	mbedtls_x509_crt_init(&ctx->cacert);

	const char* pers = "_wrapper";
	if (mbedtls_ctr_drbg_seed(&ctx->ctr_drbg, mbedtls_entropy_func, &ctx->entropy, (const unsigned char *)pers, strlen(pers)) != 0)
	{
		return 1;
	}

	if (mbedtls_x509_crt_parse(&ctx->cacert, certs_buf, certs_len) != 0)
	{
		return 2;
	}

	if (mbedtls_ssl_config_defaults(&ctx->config, MBEDTLS_SSL_IS_CLIENT, MBEDTLS_SSL_TRANSPORT_STREAM, MBEDTLS_SSL_PRESET_DEFAULT) != 0)
	{
		return 3;
	}

	mbedtls_ssl_conf_authmode(&ctx->config, MBEDTLS_SSL_VERIFY_OPTIONAL);
	mbedtls_ssl_conf_ca_chain(&ctx->config, &ctx->cacert, NULL);
	mbedtls_ssl_conf_rng(&ctx->config, mbedtls_ctr_drbg_random, &ctx->ctr_drbg);

	if (mbedtls_ssl_setup(&ctx->context, &ctx->config) != 0)
	{
		return 4;
	}

	return 0;
}

const mbedtls_ssl_context* wrapper_get_ssl_context(const struct wrapper_context* ctx)
{
	return &ctx->context;
}

void wrapper_free(struct wrapper_context* ctx)
{
	mbedtls_x509_crt_free(&ctx->cacert);
	mbedtls_ssl_config_free(&ctx->config);
	mbedtls_ssl_free(&ctx->context);
	mbedtls_ctr_drbg_free(&ctx->ctr_drbg);
	mbedtls_entropy_free(&ctx->entropy);
}

int wrapper_sizeof_wrapper_context()
{
	return sizeof(wrapper_context_t);
}
