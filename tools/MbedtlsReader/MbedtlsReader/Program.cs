using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

public class Program
{
    static Dictionary<IntPtr, Stream> _streamByPointer = new Dictionary<System.IntPtr, System.IO.Stream>();

    public static unsafe void Main(string[] args)
    {
        //var prefix = "nantoka_";
        var prefix = "";
        var arch = RuntimeInformation.OSArchitecture.ToString().ToLower();

        var mbedtls = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? $@"C:\git\guitarrapc\nativebuild-lab\artifact\mbedtls\win-{arch}-patch\mbedtls.dll" : @"/mnt/c/git/guitarrapc/nativebuild-lab/mbedtls/cmake/build.dir/library/libmbedtls.so.3.2.1";
        var mbedx509 = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? $@"C:\git\guitarrapc\nativebuild-lab\artifact\mbedtls\win-{arch}-patch\mbedx509.dll" : @"/mnt/c/git/guitarrapc/nativebuild-lab/mbedtls/cmake/build.dir/library/libmbedx509.so.3.2.1";

        if (!File.Exists(mbedtls)) throw new FileNotFoundException(mbedtls);
        if (!File.Exists(mbedx509)) throw new FileNotFoundException(mbedx509);

        var handleMbedtls = IntPtr.Zero;
        var handleMbedx509 = IntPtr.Zero;
        var f = NativeLibrary.TryLoad(mbedtls, out handleMbedtls);
        try
        {
            if (
                //NativeLibrary.TryLoad(@"C:\git\guitarrapc\nativebuild-lab\pkg\mbedtls\v3.2.1\windows\x64\mbedtls.dll", out handleMbedtls) &&
                //NativeLibrary.TryLoad(@"C:\git\guitarrapc\nativebuild-lab\pkg\mbedtls\v3.2.1\windows\x64\mbedx509.dll", out handleMbedx509)
                NativeLibrary.TryLoad(mbedtls, out handleMbedtls) &&
                NativeLibrary.TryLoad(mbedx509, out handleMbedx509))
            {
                var wrapper_init = Marshal.GetDelegateForFunctionPointer<MbedTlsNative.wrapper_init>(NativeLibrary.GetExport(handleMbedtls, "wrapper_init"));
                var wrapper_free = Marshal.GetDelegateForFunctionPointer<MbedTlsNative.wrapper_free>(NativeLibrary.GetExport(handleMbedtls, "wrapper_free"));
                var wrapper_sizeof_wrapper_context = Marshal.GetDelegateForFunctionPointer<MbedTlsNative.wrapper_sizeof_wrapper_context>(NativeLibrary.GetExport(handleMbedtls, "wrapper_sizeof_wrapper_context"));
                var wrapper_get_ssl_context = Marshal.GetDelegateForFunctionPointer<MbedTlsNative.wrapper_get_ssl_context>(NativeLibrary.GetExport(handleMbedtls, "wrapper_get_ssl_context"));

                var ctx = new byte[(int)wrapper_sizeof_wrapper_context()];

                // from mbedtls/tests/src/certs.c
                var bufCaCerts = Encoding.UTF8.GetBytes("-----BEGIN CERTIFICATE-----\r\n" +
        "MIIDQTCCAimgAwIBAgIBAzANBgkqhkiG9w0BAQsFADA7MQswCQYDVQQGEwJOTDER\r\n" +
        "MA8GA1UECgwIUG9sYXJTU0wxGTAXBgNVBAMMEFBvbGFyU1NMIFRlc3QgQ0EwHhcN\r\n" +
        "MTkwMjEwMTQ0NDAwWhcNMjkwMjEwMTQ0NDAwWjA7MQswCQYDVQQGEwJOTDERMA8G\r\n" +
        "A1UECgwIUG9sYXJTU0wxGTAXBgNVBAMMEFBvbGFyU1NMIFRlc3QgQ0EwggEiMA0G\r\n" +
        "CSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDA3zf8F7vglp0/ht6WMn1EpRagzSHx\r\n" +
        "mdTs6st8GFgIlKXsm8WL3xoemTiZhx57wI053zhdcHgH057Zk+i5clHFzqMwUqny\r\n" +
        "50BwFMtEonILwuVA+T7lpg6z+exKY8C4KQB0nFc7qKUEkHHxvYPZP9al4jwqj+8n\r\n" +
        "YMPGn8u67GB9t+aEMr5P+1gmIgNb1LTV+/Xjli5wwOQuvfwu7uJBVcA0Ln0kcmnL\r\n" +
        "R7EUQIN9Z/SG9jGr8XmksrUuEvmEF/Bibyc+E1ixVA0hmnM3oTDPb5Lc9un8rNsu\r\n" +
        "KNF+AksjoBXyOGVkCeoMbo4bF6BxyLObyavpw/LPh5aPgAIynplYb6LVAgMBAAGj\r\n" +
        "UDBOMAwGA1UdEwQFMAMBAf8wHQYDVR0OBBYEFLRa5KWz3tJS9rnVppUP6z68x/3/\r\n" +
        "MB8GA1UdIwQYMBaAFLRa5KWz3tJS9rnVppUP6z68x/3/MA0GCSqGSIb3DQEBCwUA\r\n" +
        "A4IBAQA4qFSCth2q22uJIdE4KGHJsJjVEfw2/xn+MkTvCMfxVrvmRvqCtjE4tKDl\r\n" +
        "oK4MxFOek07oDZwvtAT9ijn1hHftTNS7RH9zd/fxNpfcHnMZXVC4w4DNA1fSANtW\r\n" +
        "5sY1JB5Je9jScrsLSS+mAjyv0Ow3Hb2Bix8wu7xNNrV5fIf7Ubm+wt6SqEBxu3Kb\r\n" +
        "+EfObAT4huf3czznhH3C17ed6NSbXwoXfby7stWUDeRJv08RaFOykf/Aae7bY5PL\r\n" +
        "yTVrkAnikMntJ9YI+hNNYt3inqq11A5cN0+rVTst8UKCxzQ4GpvroSwPKTFkbMw4\r\n" +
        "/anT1dVxr/BtwJfiESoK3/4CeXR1\r\n" +
        "-----END CERTIFICATE-----\r\n" +
        // TEST_CA_CRT_RSA_SHA1_PEM
        "-----BEGIN CERTIFICATE-----\r\n" +
        "MIIDQTCCAimgAwIBAgIBAzANBgkqhkiG9w0BAQUFADA7MQswCQYDVQQGEwJOTDER\r\n" +
        "MA8GA1UECgwIUG9sYXJTU0wxGTAXBgNVBAMMEFBvbGFyU1NMIFRlc3QgQ0EwHhcN\r\n" +
        "MTEwMjEyMTQ0NDAwWhcNMjEwMjEyMTQ0NDAwWjA7MQswCQYDVQQGEwJOTDERMA8G\r\n" +
        "A1UECgwIUG9sYXJTU0wxGTAXBgNVBAMMEFBvbGFyU1NMIFRlc3QgQ0EwggEiMA0G\r\n" +
        "CSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDA3zf8F7vglp0/ht6WMn1EpRagzSHx\r\n" +
        "mdTs6st8GFgIlKXsm8WL3xoemTiZhx57wI053zhdcHgH057Zk+i5clHFzqMwUqny\r\n" +
        "50BwFMtEonILwuVA+T7lpg6z+exKY8C4KQB0nFc7qKUEkHHxvYPZP9al4jwqj+8n\r\n" +
        "YMPGn8u67GB9t+aEMr5P+1gmIgNb1LTV+/Xjli5wwOQuvfwu7uJBVcA0Ln0kcmnL\r\n" +
        "R7EUQIN9Z/SG9jGr8XmksrUuEvmEF/Bibyc+E1ixVA0hmnM3oTDPb5Lc9un8rNsu\r\n" +
        "KNF+AksjoBXyOGVkCeoMbo4bF6BxyLObyavpw/LPh5aPgAIynplYb6LVAgMBAAGj\r\n" +
        "UDBOMAwGA1UdEwQFMAMBAf8wHQYDVR0OBBYEFLRa5KWz3tJS9rnVppUP6z68x/3/\r\n" +
        "MB8GA1UdIwQYMBaAFLRa5KWz3tJS9rnVppUP6z68x/3/MA0GCSqGSIb3DQEBBQUA\r\n" +
        "A4IBAQABE3OEPfEd/bcJW5ZdU3/VgPNS4tMzh8gnJP/V2FcvFtGylMpQq6YnEBYI\r\n" +
        "yBHAL4DRvlMY5rnXGBp3ODR8MpqHC6AquRTCLzjS57iYff//4QFQqW9n92zctspv\r\n" +
        "czkaPKgjqo1No3Uq0Xaz10rcxyTUPrf5wNVRZ2V0KvllvAAVSzbI4mpdUXztjhST\r\n" +
        "S5A2BeWQAAOr0zq1F7TSRVJpJs7jmB2ai/igkh1IAjcuwV6VwlP+sbw0gjQ0NpGM\r\n" +
        "iHpnlzRAi/tIbtOvMIGOBU2TIfax/5jq1agUx5aPmT5TWAiJPOOP6l5xXnDwxeYS\r\n" +
        "NWqiX9GyusBZjezaCaHabjDLU0qQ\r\n" +
        "-----END CERTIFICATE-----\r\n" +
        // TEST_CA_CRT_EC_PEM
        "-----BEGIN CERTIFICATE-----\r\n" +
        "MIICBDCCAYigAwIBAgIJAMFD4n5iQ8zoMAwGCCqGSM49BAMCBQAwPjELMAkGA1UE\r\n" +
        "BhMCTkwxETAPBgNVBAoMCFBvbGFyU1NMMRwwGgYDVQQDDBNQb2xhcnNzbCBUZXN0\r\n" +
        "IEVDIENBMB4XDTE5MDIxMDE0NDQwMFoXDTI5MDIxMDE0NDQwMFowPjELMAkGA1UE\r\n" +
        "BhMCTkwxETAPBgNVBAoMCFBvbGFyU1NMMRwwGgYDVQQDDBNQb2xhcnNzbCBUZXN0\r\n" +
        "IEVDIENBMHYwEAYHKoZIzj0CAQYFK4EEACIDYgAEw9orNEE3WC+HVv78ibopQ0tO\r\n" +
        "4G7DDldTMzlY1FK0kZU5CyPfXxckYkj8GpUpziwth8KIUoCv1mqrId240xxuWLjK\r\n" +
        "6LJpjvNBrSnDtF91p0dv1RkpVWmaUzsgtGYWYDMeo1AwTjAMBgNVHRMEBTADAQH/\r\n" +
        "MB0GA1UdDgQWBBSdbSAkSQE/K8t4tRm8fiTJ2/s2fDAfBgNVHSMEGDAWgBSdbSAk\r\n" +
        "SQE/K8t4tRm8fiTJ2/s2fDAMBggqhkjOPQQDAgUAA2gAMGUCMFHKrjAPpHB0BN1a\r\n" +
        "LH8TwcJ3vh0AxeKZj30mRdOKBmg/jLS3rU3g8VQBHpn8sOTTBwIxANxPO5AerimZ\r\n" +
        "hCjMe0d4CTHf1gFZMF70+IqEP+o5VHsIp2Cqvflb0VGWFC5l9a4cQg==\r\n" +
        "-----END CERTIFICATE-----\r\n"
        // \0
        + "\0"
        );

                var mbedtls_ssl_set_hostname = Marshal.GetDelegateForFunctionPointer<MbedTlsNative.mbedtls_ssl_set_hostname>(NativeLibrary.GetExport(handleMbedtls, prefix + "mbedtls_ssl_set_hostname"));
                var mbedtls_ssl_set_bio = Marshal.GetDelegateForFunctionPointer<MbedTlsNative.mbedtls_ssl_set_bio>(NativeLibrary.GetExport(handleMbedtls, prefix + "mbedtls_ssl_set_bio"));
                var mbedtls_ssl_handshake = Marshal.GetDelegateForFunctionPointer<MbedTlsNative.mbedtls_ssl_handshake>(NativeLibrary.GetExport(handleMbedtls, prefix + "mbedtls_ssl_handshake"));
                var mbedtls_ssl_write = Marshal.GetDelegateForFunctionPointer<MbedTlsNative.mbedtls_ssl_write>(NativeLibrary.GetExport(handleMbedtls, prefix + "mbedtls_ssl_write"));
                var mbedtls_ssl_read = Marshal.GetDelegateForFunctionPointer<MbedTlsNative.mbedtls_ssl_read>(NativeLibrary.GetExport(handleMbedtls, prefix + "mbedtls_ssl_read"));

                var handleContext = GCHandle.Alloc(ctx, GCHandleType.Pinned);
                var handleCaCerts = GCHandle.Alloc(bufCaCerts, GCHandleType.Pinned);
                try
                {
                    var pCtx = handleContext.AddrOfPinnedObject();
                    wrapper_init(pCtx, handleCaCerts.AddrOfPinnedObject(), (IntPtr)bufCaCerts.Length);
                    try
                    {
                        using var tcpClient = new TcpClient();
                        tcpClient.Connect("www.example.com", 443);

                        using var stream = tcpClient.GetStream();
                        _streamByPointer[pCtx] = stream;

                        mbedtls_ssl_set_hostname(pCtx, "www.example.com");
                        mbedtls_ssl_set_bio(pCtx, pCtx,
                            Marshal.GetFunctionPointerForDelegate<MbedTlsNative.mbedtls_ssl_send_t>(mbedtls_ssl_send),
                            Marshal.GetFunctionPointerForDelegate<MbedTlsNative.mbedtls_ssl_recv_t>(mbedtls_ssl_recv),
                            Marshal.GetFunctionPointerForDelegate<MbedTlsNative.mbedtls_ssl_recv_timeout_t>(mbedtls_ssl_timeout)
                        );

                        Console.WriteLine("Handshaking...");
                        var ret = 0;
                        while ((ret = mbedtls_ssl_handshake(pCtx)) != 0)
                        {
                            throw new InvalidOperationException($"mbedtls_ssl_handshake returns {ret} ({ToHexErrorCode(ret)})");
                        }
                        Console.WriteLine("Handshaking Done.");

                        var getRequest = "GET / HTTP/1.0\r\nHost: www.example.com\r\n\r\n";
                        var requestBuffer = Encoding.UTF8.GetBytes(getRequest);
                        var handleRequestBuffer = GCHandle.Alloc(requestBuffer, GCHandleType.Pinned);
                        try
                        {
                            Console.WriteLine("Sending...");
                            while ((ret = mbedtls_ssl_write(pCtx, handleRequestBuffer.AddrOfPinnedObject().ToPointer(), (IntPtr)requestBuffer.Length)) <= 0)
                            {
                                throw new InvalidOperationException($"mbedtls_ssl_write returns {ret} ({ToHexErrorCode(ret)})");
                            }
                            Console.WriteLine("Sending Done.");
                        }
                        finally
                        {
                            handleRequestBuffer.Free();
                        }

                        Span<byte> receiveBuffer = stackalloc byte[1024 * 32];
                        var receiveBufferPtr = Unsafe.AsPointer(ref receiveBuffer.GetPinnableReference());
                        Console.WriteLine("Reading...");

                        while (true)
                        {
                            ret = mbedtls_ssl_read(pCtx, receiveBufferPtr, (IntPtr)receiveBuffer.Length);
                            if (ret == -0x7880 /* MBEDTLS_ERR_SSL_PEER_CLOSE_NOTIFY */) break;
                            if (ret < 0) throw new InvalidOperationException($"mbedtls_ssl_read returns {ret} ({ToHexErrorCode(ret)})");
                            if (ret == 0) break;

                            Console.Write(Encoding.UTF8.GetString(receiveBuffer.Slice(0, ret)));
                        }

                        Console.WriteLine("Reading Done.");

                    }
                    finally
                    {
                        wrapper_free(pCtx);
                    }
                }
                finally
                {
                    handleContext.Free();
                    handleCaCerts.Free();
                }
            }
        }
        finally
        {
            if (handleMbedtls != IntPtr.Zero)
            {
                NativeLibrary.Free(handleMbedtls);
            }
            if (handleMbedx509 != IntPtr.Zero)
            { 
                NativeLibrary.Free(handleMbedx509);
            }
        }
    }

    unsafe static int mbedtls_ssl_send(IntPtr ctx, IntPtr buf, IntPtr len)
    {
        var stream = _streamByPointer[ctx];
        stream.Write(new Span<byte>(buf.ToPointer(), (int)len));
        return (int)len;
    }
    unsafe static int mbedtls_ssl_recv(IntPtr ctx, IntPtr buf, IntPtr len)
    {
        var stream = _streamByPointer[ctx];
        var read = stream.Read(new Span<byte>(buf.ToPointer(), (int)len));
        return read;
    }
    unsafe static int mbedtls_ssl_timeout(IntPtr ctx, IntPtr buf, IntPtr len, int timeout)
    {
        var stream = _streamByPointer[ctx];
        var read = stream.Read(new Span<byte>(buf.ToPointer(), (int)len));
        return read;
    }

    unsafe class MbedTlsNative
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public delegate int mbedtls_ssl_send_t(IntPtr ctx, IntPtr buf, IntPtr len);
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public delegate int mbedtls_ssl_recv_t(IntPtr ctx, IntPtr buf, IntPtr len);
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public delegate int mbedtls_ssl_recv_timeout_t(IntPtr ctx, IntPtr buf, IntPtr len, int timeout);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public delegate int wrapper_init(IntPtr ctx, IntPtr caCerts, IntPtr bufferLen);
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public delegate void wrapper_free(IntPtr ctx);
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public delegate IntPtr wrapper_sizeof_wrapper_context();
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public delegate IntPtr wrapper_get_ssl_context(IntPtr ctx);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public delegate int mbedtls_ssl_set_hostname(IntPtr ssl, string hostname);
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public delegate void mbedtls_ssl_set_bio(IntPtr ssl, IntPtr p_bio, IntPtr f_send, IntPtr f_recv, IntPtr f_recv_timeout);
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public delegate int mbedtls_ssl_handshake(IntPtr ssl);
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public delegate int mbedtls_ssl_write(IntPtr ssl, void* buf, IntPtr len);
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public delegate int mbedtls_ssl_read(IntPtr ssl, void* buf, IntPtr len);
    }

    static string ToHexErrorCode(int ret)
    {
        return $"-0x{(Math.Abs(ret)).ToString("X")}";
    }
}
