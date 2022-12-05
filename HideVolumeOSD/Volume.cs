

using System;
using System.Runtime.InteropServices;
public class Volume
{
    public enum HRESULT : int
    {
        S_OK = 0,
        S_FALSE = 1,
        E_NOINTERFACE = unchecked((int)0x80004002),
        E_NOTIMPL = unchecked((int)0x80004001),
        E_FAIL = unchecked((int)0x80004005)
    }
    [ComImport, Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPropertyStore
    {
        HRESULT GetCount([Out] out uint propertyCount);
        HRESULT GetAt([In] uint propertyIndex, [Out, MarshalAs(UnmanagedType.Struct)] out PROPERTYKEY key);
        HRESULT GetValue([In, MarshalAs(UnmanagedType.Struct)] ref PROPERTYKEY key, [Out, MarshalAs(UnmanagedType.Struct)] out PROPVARIANT pv);
        HRESULT SetValue([In, MarshalAs(UnmanagedType.Struct)] ref PROPERTYKEY key, [In, MarshalAs(UnmanagedType.Struct)] ref PROPVARIANT pv);
        HRESULT Commit();
    }
    public const int STGM_READ = 0x00000000;
    public const int STGM_WRITE = 0x00000001;
    public const int STGM_READWRITE = 0x00000002;
    public enum EDataFlow
    {
        eRender = 0,
        eCapture = (eRender + 1),
        eAll = (eCapture + 1),
        EDataFlow_enum_count = (eAll + 1)
    }
    public enum ERole
    {
        eConsole = 0,
        eMultimedia = (eConsole + 1),
        eCommunications = (eMultimedia + 1),
        ERole_enum_count = (eCommunications + 1)
    }
    [ComImport]
    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDeviceEnumerator
    {
        HRESULT EnumAudioEndpoints(EDataFlow dataFlow, int dwStateMask, out IMMDeviceCollection ppDevices);
        HRESULT GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, out IMMDevice ppEndpoint);
        HRESULT GetDevice(string pwstrId, out IMMDevice ppDevice);
        HRESULT RegisterEndpointNotificationCallback(IMMNotificationClient pClient);
        HRESULT UnregisterEndpointNotificationCallback(IMMNotificationClient pClient);
    }
    public const int DEVICE_STATE_ACTIVE = 0x00000001;
    public const int DEVICE_STATE_DISABLED = 0x00000002;
    public const int DEVICE_STATE_NOTPRESENT = 0x00000004;
    public const int DEVICE_STATE_UNPLUGGED = 0x00000008;
    public const int DEVICE_STATEMASK_ALL = 0x0000000f;
    [ComImport]
    [Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDeviceCollection
    {
        HRESULT GetCount(out uint pcDevices);
        HRESULT Item(uint nDevice, out IMMDevice ppDevice);
    }
    [ComImport]
    [Guid("D666063F-1587-4E43-81F1-B948E807363F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDevice
    {
        [PreserveSig]
        HRESULT Activate(ref Guid iid, int dwClsCtx, ref PROPVARIANT pActivationParams, out IntPtr ppInterface);
        //HRESULT Activate(ref Guid iid, int dwClsCtx, IntPtr pActivationParams, out IntPtr ppInterface);

        HRESULT OpenPropertyStore(int stgmAccess, out IPropertyStore ppProperties);
        //HRESULT GetId(StringBuilder ppstrId);
        HRESULT GetId(out IntPtr ppstrId);
        HRESULT GetState(out int pdwState);
    }
    [ComImport]
    [Guid("7991EEC9-7E89-4D85-8390-6C703CEC60C0")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMNotificationClient
    {
        HRESULT OnDeviceStateChanged(string pwstrDeviceId, int dwNewState);
        HRESULT OnDeviceAdded(string pwstrDeviceId);
        HRESULT OnDeviceRemoved(string pwstrDeviceId);
        HRESULT OnDefaultDeviceChanged(EDataFlow flow, ERole role, string pwstrDefaultDeviceId);
        HRESULT OnPropertyValueChanged(string pwstrDeviceId, ref PROPERTYKEY key);
    }
    [ComImport]
    [Guid("1BE09788-6894-4089-8586-9A2A6C265AC5")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMEndpoint
    {
        HRESULT GetDataFlow(out EDataFlow pDataFlow);
    }
    public struct PROPERTYKEY
    {
        public PROPERTYKEY(Guid InputId, UInt32 InputPid)
        {
            fmtid = InputId;
            pid = InputPid;
        }
        Guid fmtid;
        uint pid;
    };
    [StructLayout(LayoutKind.Sequential)]
    public struct PROPARRAY
    {
        public UInt32 cElems;
        public IntPtr pElems;
    }
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct PROPVARIANT
    {
        [FieldOffset(0)]
        public ushort varType;
        [FieldOffset(2)]
        public ushort wReserved1;
        [FieldOffset(4)]
        public ushort wReserved2;
        [FieldOffset(6)]
        public ushort wReserved3;
        [FieldOffset(8)]
        public byte bVal;
        [FieldOffset(8)]
        public sbyte cVal;
        [FieldOffset(8)]
        public ushort uiVal;
        [FieldOffset(8)]
        public short iVal;
        [FieldOffset(8)]
        public UInt32 uintVal;
        [FieldOffset(8)]
        public Int32 intVal;
        [FieldOffset(8)]
        public UInt64 ulVal;
        [FieldOffset(8)]
        public Int64 lVal;
        [FieldOffset(8)]
        public float fltVal;
        [FieldOffset(8)]
        public double dblVal;
        [FieldOffset(8)]
        public short boolVal;
        [FieldOffset(8)]
        public IntPtr pclsidVal; // GUID ID pointer
        [FieldOffset(8)]
        public IntPtr pszVal; // Ansi string pointer
        [FieldOffset(8)]
        public IntPtr pwszVal; // Unicode string pointer
        [FieldOffset(8)]
        public IntPtr punkVal; // punkVal (interface pointer)
        [FieldOffset(8)]
        public PROPARRAY ca;
        [FieldOffset(8)]
        public System.Runtime.InteropServices.ComTypes.FILETIME filetime;
    }
    [ComImport]
    [Guid("5CDF2C82-841E-4546-9722-0CF74078229A")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioEndpointVolume
    {
        HRESULT RegisterControlChangeNotify(IAudioEndpointVolumeCallback pNotify);
        HRESULT UnregisterControlChangeNotify(IAudioEndpointVolumeCallback pNotify);
        HRESULT GetChannelCount(out uint pnChannelCount);
        HRESULT SetMasterVolumeLevel(float fLevelDB, ref Guid pguidEventContext);
        HRESULT SetMasterVolumeLevelScalar(float fLevel, ref Guid pguidEventContext);
        HRESULT GetMasterVolumeLevel(out float pfLevelDB);
        HRESULT GetMasterVolumeLevelScalar(out float pfLevel);
        HRESULT SetChannelVolumeLevel(uint nChannel, float fLevelDB, ref Guid pguidEventContext);
        HRESULT SetChannelVolumeLevelScalar(uint nChannel, float fLevel, ref Guid pguidEventContext);
        HRESULT GetChannelVolumeLevel(uint nChannel, out float pfLevelDB);
        HRESULT GetChannelVolumeLevelScalar(uint nChannel, out float pfLevel);
        HRESULT SetMute(bool bMute, ref Guid pguidEventContext);
        HRESULT GetMute(out bool pbMute);
        HRESULT GetVolumeStepInfo(out uint pnStep, out uint pnStepCount);
        HRESULT VolumeStepUp(ref Guid pguidEventContext);
        HRESULT VolumeStepDown(ref Guid pguidEventContext);
        HRESULT QueryHardwareSupport(out int pdwHardwareSupportMask);
        HRESULT GetVolumeRange(out float pflVolumeMindB, out float pflVolumeMaxdB, out float pflVolumeIncrementdB);
    }
    [ComImport]
    [Guid("657804FA-D6AD-4496-8A60-352752AF4F89")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioEndpointVolumeCallback
    {
        HRESULT OnNotify(IntPtr pNotify);
    }

    public float GetMasterVolume()
    {
        HRESULT hr = HRESULT.E_FAIL;
        Guid CLSID_MMDeviceEnumerator = new Guid("{BCDE0395-E52F-467C-8E3D-C4579291692E}");
        Type MMDeviceEnumeratorType = Type.GetTypeFromCLSID(CLSID_MMDeviceEnumerator, true);
        object MMDeviceEnumerator = Activator.CreateInstance(MMDeviceEnumeratorType);
        IMMDeviceEnumerator pMMDeviceEnumerator = (IMMDeviceEnumerator)MMDeviceEnumerator;

        float masterVolume = 0;

        if (pMMDeviceEnumerator != null)
        {
            IMMDevice pDevice = null;

            hr = pMMDeviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eConsole, out pDevice);

            if (hr == HRESULT.S_OK)
            {
                IPropertyStore pPropertyStore = null;

                hr = pDevice.OpenPropertyStore(STGM_READ, out pPropertyStore);

                if (hr == HRESULT.S_OK)
                {
                    IntPtr pAudioEndpointVolumePtr = IntPtr.Zero;
                    PROPVARIANT pvActivate = new PROPVARIANT();
                    hr = pDevice.Activate(typeof(IAudioEndpointVolume).GUID, 0, ref pvActivate, out pAudioEndpointVolumePtr);
                    
                    if (hr == HRESULT.S_OK)
                    {
                        IAudioEndpointVolume pAudioEndpointVolume = Marshal.GetObjectForIUnknown(pAudioEndpointVolumePtr) as IAudioEndpointVolume;
                        pAudioEndpointVolume.GetMasterVolumeLevelScalar(out masterVolume);
                        Marshal.ReleaseComObject(pAudioEndpointVolume);
                    }
                    
                    Marshal.ReleaseComObject(pPropertyStore);
                }
                
                Marshal.ReleaseComObject(pDevice);
            }               
        }

        return masterVolume;
    }
}