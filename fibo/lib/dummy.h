    typedef uint64_t mbedtls_mpi_uint;
      typedef uint64_t piyopiyo;
    // typedef uint64_t should_be_ignopre_typdef;

    typedef uint64_t mbedtls_mpi_uint

    typedef enum {
        MBEDTLS_SSL_MODE_STREAM = 0,
        MBEDTLS_SSL_MODE_CBC,
        MBEDTLS_SSL_MODE_CBC_ETM,
        MBEDTLS_SSL_MODE_AEAD
    } mbedtls_ssl_mode_t;

        typedef struct
        {
            void *key;
            mbedtls_pk_rsa_alt_decrypt_func decrypt_func;
            mbedtls_pk_rsa_alt_sign_func sign_func;
            mbedtls_pk_rsa_alt_key_len_func key_len_func;
        } mbedtls_rsa_alt_context;

    // typedef enum {
        MBEDTLS_SSL_MODE_STREAM = 0,
        MBEDTLS_SSL_MODE_CBC,
        MBEDTLS_SSL_MODE_CBC_ETM,
        MBEDTLS_SSL_MODE_AEAD
    } should_be_ignopre_typdef;

    void foo();
       int mbedtls_ssl_write_client_hello( mbedtls_ssl_context *ssl );

    FIBOLIB_API void get_sample_data(sample_data_t *output);
      FIBOLIB_API int fibo(int n);

    mbedtls_ssl_mode_t mbedtls_ssl_get_mode_from_transform(
            const mbedtls_ssl_transform *transform );
        mbedtls_mpi_uint mbedtls_mpi_core_mla( mbedtls_mpi_uint *d, size_t d_len ,
                                           const mbedtls_mpi_uint *s, size_t s_len,
                                           mbedtls_mpi_uint b );

    // void should_be_ignopre_method();
    // FIBOLIB_API void should_be_ignopre_method(sample_data_t *output);
    // mbedtls_ssl_mode_t should_be_ignopre_method(
            const mbedtls_ssl_transform *transform );

    /*
     * GCM multiplication: c = a times b in GF(2^128)
     * Based on [CLMUL-WP] algorithms 1 (with equation 27) and 5.
     */

    #ifndef FIBOLIB_VISIBILITY
    #  if defined(__GNUC__) && (__GNUC__ >= 4)
    #    define FIBOLIB_VISIBILITY __attribute__ ((visibility ("default")))
    #  else
    #    define FIBOLIB_VISIBILITY
    #  endif
    #endif

/*
* GCM multiplication: c = a times b in GF(2^128)
* Based on [CLMUL-WP] algorithms 1 (with equation 27) and 5.
*/
