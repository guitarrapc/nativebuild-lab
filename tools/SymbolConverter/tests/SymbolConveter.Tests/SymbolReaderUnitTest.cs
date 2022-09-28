using FluentAssertions;

namespace SymbolConveter.Tests;

public class SymbolReaderUnitTest
{
    const string PREFIX = "THISIS_PREFIX_";

    // complex
    [Fact]
    public void ReadTest()
    {
        var content = @"
        typedef uint64_t mbedtls_mpi_uint;
          typedef uint64_t piyopiyo;
        //  typedef uint64_t should_be_ignopre_typdef;
        
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

        #ifndef FIBOLIB_VISIBILITY
        #  if defined(__GNUC__) && (__GNUC__ >= 4)
        #    define FIBOLIB_VISIBILITY __attribute__ ((visibility (""default"")))
        #  else
        #    define FIBOLIB_VISIBILITY
        #  endif
        #endif
".SplitNewLine();

        var reader = new SymbolReader();

        // method
        {
            var actual = reader.Read(DetectionType.Method, content, s => PREFIX + s);
            actual.Should().NotBeEmpty();
            actual.Count().Should().Be(6);
        }

        // typedef
        {
            var actual = reader.Read(DetectionType.Typedef, content, s => PREFIX + s);
            actual.Should().NotBeEmpty();
            actual.Count().Should().Be(4);
        }
    }

    // method
    [Theory]
    [InlineData(@"void foo();", "foo", PREFIX + "foo")]
    [InlineData(@"   int mbedtls_ssl_write_client_hello( mbedtls_ssl_context *ssl );", "mbedtls_ssl_write_client_hello", PREFIX + "mbedtls_ssl_write_client_hello")]
    [InlineData(@"FIBOLIB_API void get_sample_data(sample_data_t *output);", "get_sample_data", PREFIX + "get_sample_data")]
    [InlineData(@"  FIBOLIB_API int fibo(int n);", "fibo", PREFIX + "fibo")]
    [InlineData(@"mbedtls_ssl_mode_t mbedtls_ssl_get_mode_from_transform(;        ", "mbedtls_ssl_get_mode_from_transform", PREFIX + "mbedtls_ssl_get_mode_from_transform")]
    [InlineData(@"    mbedtls_mpi_uint mbedtls_mpi_core_mla( mbedtls_mpi_uint *d, size_t d_len ,", "mbedtls_mpi_core_mla", PREFIX + "mbedtls_mpi_core_mla")]
    public void ReadMethodTest(string define, string expectedSymbol, string expectedRenamedSymbol)
    {
        var content = new[] { define };
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Method, content, s => PREFIX + s);

        actuals.Should().NotBeEmpty();
        foreach (var actual in actuals)
        {
            actual.Should().NotBeNull();
            actual!.Symbol.Should().Be(expectedSymbol);
            actual!.RenamedSymbol.Should().Be(expectedRenamedSymbol);
            actual!.Delimiter!.Should().Be("(");
        }
    }

    [Theory]
    [InlineData(@"// void should_be_ignopre_method();")]
    [InlineData(@"// FIBOLIB_API void should_be_ignopre_method(sample_data_t *output);")]
    [InlineData(@"// mbedtls_ssl_mode_t should_be_ignopre_method(
                const mbedtls_ssl_transform *transform );")]
    public void ReadMethodCommentTest(string define)
    {
        var content = new[] { define };
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Method, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }

    [Theory]
    [InlineData(@"const mbedtls_ssl_transform *transform )")]
    [InlineData(@"constconst mbedtls_mpi_uint *s, size_t s_len,")]
    [InlineData(@"const                                          mbedtls_mpi_uint b );        ")]
    [InlineData(@"#ifndef FIBOLIB_VISIBILITY")]
    [InlineData(@"#  if defined(__GNUC__) && (__GNUC__ >= 4)")]
    [InlineData(@"#    define FIBOLIB_VISIBILITY __attribute__ ((visibility (""default"")))")]
    [InlineData(@"#  else")]
    [InlineData(@"#    define FIBOLIB_VISIBILITY")]
    [InlineData(@"#  endif")]
    [InlineData(@"#endif")]
    public void ReadMethodInvalidTest(string define)
    {
        var content = new[] { define };
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Method, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }

    // typdef
    [Theory]
    [InlineData(@"typedef uint64_t mbedtls_mpi_uint;", "mbedtls_mpi_uint", PREFIX + "mbedtls_mpi_uint")]
    [InlineData(@"  typedef uint64_t piyopiyo;", "piyopiyo", PREFIX + "piyopiyo")]
    [InlineData(@"typedef enum {
            MBEDTLS_SSL_MODE_STREAM = 0,
            MBEDTLS_SSL_MODE_CBC,
            MBEDTLS_SSL_MODE_CBC_ETM,
            MBEDTLS_SSL_MODE_AEAD
        } mbedtls_ssl_mode_t;", "mbedtls_ssl_mode_t", PREFIX + "mbedtls_ssl_mode_t")]
    [InlineData(@"    typedef struct
            {
                void *key;
                mbedtls_pk_rsa_alt_decrypt_func decrypt_func;
                mbedtls_pk_rsa_alt_sign_func sign_func;
                mbedtls_pk_rsa_alt_key_len_func key_len_func;
            } mbedtls_rsa_alt_context;", "mbedtls_rsa_alt_context", PREFIX + "mbedtls_rsa_alt_context")]
    public void ReadTypedefTest(string define, string expectedSymbol, string expectedRenamedSymbol)
    {
        var content = new[] { define };
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Typedef, content, s => PREFIX + s);

        actuals.Should().NotBeEmpty();
        foreach (var actual in actuals)
        {
            actual.Should().NotBeNull();
            actual!.Symbol.Should().Be(expectedSymbol);
            actual!.RenamedSymbol.Should().Be(expectedRenamedSymbol);
            actual!.Delimiter!.Should().Be(";");
        }
    }

    [Theory]
    [InlineData(@"//  typedef uint64_t should_be_ignopre_typdef;")]
    [InlineData(@"// typedef enum {
            MBEDTLS_SSL_MODE_STREAM = 0,
            MBEDTLS_SSL_MODE_CBC,
            MBEDTLS_SSL_MODE_CBC_ETM,
            MBEDTLS_SSL_MODE_AEAD
        } should_be_ignopre_typdef;")]
    public void ReadTypedefCommentTest(string define)
    {
        var content = new[] { define };
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Typedef, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }

    [Theory]
    [InlineData(@"typedef uint64_t mbedtls_mpi_uint")] // missing end semi-colon
    [InlineData(@"typedef enum {
            MBEDTLS_SSL_MODE_STREAM = 0,
            MBEDTLS_SSL_MODE_CBC,
            MBEDTLS_SSL_MODE_CBC_ETM,
            MBEDTLS_SSL_MODE_AEAD
        } mbedtls_ssl_mode_t")] // missing end semi-colon
    [InlineData(@"typede uint64_t mbedtls_mpi_uint;")] // typede
    public void ReadTypedefInvalidTest(string define)
    {
        var content = new[] { define };
        var reader = new SymbolReader();
        var actuals = reader.Read(DetectionType.Method, content, s => PREFIX + s);

        actuals.Should().BeEmpty();
    }
}
