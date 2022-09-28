typedef uint64_t mbedtls_mpi_uint;
  typedef uint64_t piyopiyo;
//  typedef uint64_t should_be_ignopre_typdef;

    typedef struct
    {
        void *key;
        mbedtls_pk_rsa_alt_decrypt_func decrypt_func;
        mbedtls_pk_rsa_alt_sign_func sign_func;
        mbedtls_pk_rsa_alt_key_len_func key_len_func;
    } mbedtls_rsa_alt_context;

typedef enum {
    MBEDTLS_SSL_MODE_STREAM = 0,
    MBEDTLS_SSL_MODE_CBC,
    MBEDTLS_SSL_MODE_CBC_ETM,
    MBEDTLS_SSL_MODE_AEAD
} mbedtls_ssl_mode_t;
mbedtls_ssl_mode_t mbedtls_ssl_get_mode_from_transform(
        const mbedtls_ssl_transform *transform );

mbedtls_mpi_uint mbedtls_mpi_core_mla( mbedtls_mpi_uint *d, size_t d_len ,
                                       const mbedtls_mpi_uint *s, size_t s_len,
                                       mbedtls_mpi_uint b );

   int mbedtls_ssl_write_client_hello( mbedtls_ssl_context *ssl );
void foo();

FIBOLIB_API void get_sample_data(sample_data_t *output);
FIBOLIB_API int fibo(int n);
// void should_be_ignopre_method();

#ifndef FIBOLIB_VISIBILITY
#  if defined(__GNUC__) && (__GNUC__ >= 4)
#    define FIBOLIB_VISIBILITY __attribute__ ((visibility ("default")))
#  else
#    define FIBOLIB_VISIBILITY
#  endif
#endif
#if defined(DLL_EXPORT)
#  define FIBOLIB_API __declspec(dllexport) FIBOLIB_VISIBILITY
#elif defined(DLL_IMPORT)
#  define FIBOLIB_API __declspec(dllimport) FIBOLIB_VISIBILITY /* It isn't required but allows to generate better code, saving a function pointer load from the IAT and an indirect jump.*/
#else
#  define FIBOLIB_API FIBOLIB_VISIBILITY
#endif
