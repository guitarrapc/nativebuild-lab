extern const foo_info_t foo_info;
  extern int bar_int;
extern void (*function_pointer_foo)( const char * test, int line, const char * file );
  extern int ( *function_pointer_bar )();
extern void* (function_pointer_piyo)( );
// extern const int comment_out_field;

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
        struct key_data
        {
            uint8_t *data;
            size_t bytes;
        } inside;
    } nested_t;

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

typedef struct
{
    struct key_data
    {
        uint8_t *data;
        size_t bytes;
    } key;
} psa_key_slot_t;

void foo();
    int mbedtls_ssl_write_client_hello( mbedtls_ssl_context *ssl );

FIBOLIB_API void get_sample_data(sample_data_t *output);
    FIBOLIB_API int fibo(int n);

mbedtls_ssl_mode_t mbedtls_ssl_get_mode_from_transform(
        const mbedtls_ssl_transform *transform );
    mbedtls_mpi_uint mbedtls_mpi_core_mla( mbedtls_mpi_uint *d, size_t d_len ,
                                        const mbedtls_mpi_uint *s, size_t s_len,
                                        mbedtls_mpi_uint b );

struct foo_struct
{
    unsigned char *p;       /*!< message, including handshake headers   */
    size_t len;             /*!< length of p                            */
    unsigned char type;     /*!< type of the message: handshake or CCS  */
    mbedtls_ssl_flight_item *next;  /*!< next handshake message(s)              */
};

static inline int mbedtls_ssl_get_psk( const mbedtls_ssl_context *ssl,
    const unsigned char **psk, size_t *psk_len )
{
    if( ssl->handshake->psk != NULL && ssl->handshake->psk_len > 0 )
    {
        *psk = ssl->handshake->psk;
        *psk_len = ssl->handshake->psk_len;
    }

    else if( ssl->conf->psk != NULL && ssl->conf->psk_len > 0 )
    {
        *psk = ssl->conf->psk;
        *psk_len = ssl->conf->psk_len;
    }

    else
    {
        *psk = NULL;
        *psk_len = 0;
        return( MBEDTLS_ERR_SSL_PRIVATE_KEY_REQUIRED );
    }

    return( 0 );
}

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

#define MBEDTLS_BYTES_TO_T_UINT_8( a, b, c, d, e, f, g, h ) \
    MBEDTLS_BYTES_TO_T_UINT_4( a, b, c, d ),                \
    MBEDTLS_BYTES_TO_T_UINT_4( e, f, g, h )

#define MBEDTLS_BYTES_TO_T_UINT_8( a, b, c, d, e, f, g, h )   \
    ( (mbedtls_mpi_uint) (a) <<  0 ) |                        \
    ( (mbedtls_mpi_uint) (b) <<  8 ) |                        \
    ( (mbedtls_mpi_uint) (c) << 16 ) |                        \
    ( (mbedtls_mpi_uint) (d) << 24 ) |                        \
    ( (mbedtls_mpi_uint) (e) << 32 ) |                        \
    ( (mbedtls_mpi_uint) (f) << 40 ) |                        \
    ( (mbedtls_mpi_uint) (g) << 48 ) |                        \
    ( (mbedtls_mpi_uint) (h) << 56 )

#define MULADDC_X8_CORE                         \
    "movd     %%ecx,     %%mm1      \n\t"   \
    "movd     %%ebx,     %%mm0      \n\t"   \
    "movd     (%%edi),   %%mm3      \n\t"   \
    "paddq    %%mm3,     %%mm1      \n\t"   \
    "movd     (%%esi),   %%mm2      \n\t"   \
    "pmuludq  %%mm0,     %%mm2      \n\t"   \
    "movd     4(%%esi),  %%mm4      \n\t"   \
    "pmuludq  %%mm0,     %%mm4      \n\t"   \
    "movd     8(%%esi),  %%mm6      \n\t"   \
    "pmuludq  %%mm0,     %%mm6      \n\t"   \
    "movd     12(%%esi), %%mm7      \n\t"   \
    "pmuludq  %%mm0,     %%mm7      \n\t"   \
    "paddq    %%mm2,     %%mm1      \n\t"   \
    "movd     4(%%edi),  %%mm3      \n\t"   \
    "paddq    %%mm4,     %%mm3      \n\t"   \
    "movd     8(%%edi),  %%mm5      \n\t"   \
    "paddq    %%mm6,     %%mm5      \n\t"   \
    "movd     12(%%edi), %%mm4      \n\t"   \
    "paddq    %%mm4,     %%mm7      \n\t"   \
    "movd     %%mm1,     (%%edi)    \n\t"   \
    "movd     16(%%esi), %%mm2      \n\t"   \
    "pmuludq  %%mm0,     %%mm2      \n\t"   \
    "psrlq    $32,       %%mm1      \n\t"   \
    "movd     20(%%esi), %%mm4      \n\t"   \
    "pmuludq  %%mm0,     %%mm4      \n\t"   \
    "paddq    %%mm3,     %%mm1      \n\t"   \
    "movd     24(%%esi), %%mm6      \n\t"   \
    "pmuludq  %%mm0,     %%mm6      \n\t"   \
    "movd     %%mm1,     4(%%edi)   \n\t"   \
    "psrlq    $32,       %%mm1      \n\t"   \
    "movd     28(%%esi), %%mm3      \n\t"   \
    "pmuludq  %%mm0,     %%mm3      \n\t"   \
    "paddq    %%mm5,     %%mm1      \n\t"   \
    "movd     16(%%edi), %%mm5      \n\t"   \
    "paddq    %%mm5,     %%mm2      \n\t"   \
    "movd     %%mm1,     8(%%edi)   \n\t"   \
    "psrlq    $32,       %%mm1      \n\t"   \
    "paddq    %%mm7,     %%mm1      \n\t"   \
    "movd     20(%%edi), %%mm5      \n\t"   \
    "paddq    %%mm5,     %%mm4      \n\t"   \
    "movd     %%mm1,     12(%%edi)  \n\t"   \
    "psrlq    $32,       %%mm1      \n\t"   \
    "paddq    %%mm2,     %%mm1      \n\t"   \
    "movd     24(%%edi), %%mm5      \n\t"   \
    "paddq    %%mm5,     %%mm6      \n\t"   \
    "movd     %%mm1,     16(%%edi)  \n\t"   \
    "psrlq    $32,       %%mm1      \n\t"   \
    "paddq    %%mm4,     %%mm1      \n\t"   \
    "movd     28(%%edi), %%mm5      \n\t"   \
    "paddq    %%mm5,     %%mm3      \n\t"   \
    "movd     %%mm1,     20(%%edi)  \n\t"   \
    "psrlq    $32,       %%mm1      \n\t"   \
    "paddq    %%mm6,     %%mm1      \n\t"   \
    "movd     %%mm1,     24(%%edi)  \n\t"   \
    "psrlq    $32,       %%mm1      \n\t"   \
    "paddq    %%mm3,     %%mm1      \n\t"   \
    "movd     %%mm1,     28(%%edi)  \n\t"   \
    "addl     $32,       %%edi      \n\t"   \
    "addl     $32,       %%esi      \n\t"   \
    "psrlq    $32,       %%mm1      \n\t"   \
    "movd     %%mm1,     %%ecx      \n\t"
