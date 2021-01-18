using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using Crestron;
using Crestron.Logos.SplusLibrary;
using Crestron.Logos.SplusObjects;
using Crestron.SimplSharp;

namespace CrestronModule_DYNAMIC_PRESET_V1_1_0
{
    public class CrestronModuleClass_DYNAMIC_PRESET_V1_1_0 : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        
        
        
        
        Crestron.Logos.SplusObjects.DigitalInput RECALL;
        Crestron.Logos.SplusObjects.DigitalInput SAVE;
        Crestron.Logos.SplusObjects.DigitalInput DELETE;
        Crestron.Logos.SplusObjects.StringInput PATH__DOLLAR__;
        Crestron.Logos.SplusObjects.StringInput FILENAME__DOLLAR__;
        InOutArray<Crestron.Logos.SplusObjects.AnalogInput> LEVEL_IN;
        Crestron.Logos.SplusObjects.DigitalOutput RECALL_OK;
        Crestron.Logos.SplusObjects.DigitalOutput RECALL_ERROR;
        Crestron.Logos.SplusObjects.DigitalOutput SAVE_OK;
        Crestron.Logos.SplusObjects.DigitalOutput SAVE_ERROR;
        Crestron.Logos.SplusObjects.DigitalOutput DELETE_OK;
        Crestron.Logos.SplusObjects.DigitalOutput DELETE_ERROR;
        InOutArray<Crestron.Logos.SplusObjects.AnalogOutput> LEVEL_OUT;
        ushort G_IMAXLEVELS = 0;
        CrestronString M_SFILENAME;
        object RECALL_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                short IRESULT = 0;
                short IFILEHANDLE = 0;
                
                ushort I = 0;
                ushort ILEVEL = 0;
                
                FILE_INFO FI;
                FI  = new FILE_INFO();
                FI .PopulateDefaults();
                
                
                __context__.SourceCodeLine = 59;
                StartFileOperations ( ) ; 
                __context__.SourceCodeLine = 61;
                IRESULT = (short) ( FindFirst( M_SFILENAME , ref FI ) ) ; 
                __context__.SourceCodeLine = 63;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( IRESULT < 0 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 66;
                    
                    __context__.SourceCodeLine = 69;
                    Functions.Pulse ( 50, RECALL_ERROR ) ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 76;
                    IFILEHANDLE = (short) ( FileOpen( M_SFILENAME ,(ushort) (0 | 32768) ) ) ; 
                    __context__.SourceCodeLine = 78;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( IFILEHANDLE >= 0 ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 80;
                        I = (ushort) ( 1 ) ; 
                        __context__.SourceCodeLine = 82;
                        while ( Functions.TestForTrue  ( ( Functions.Not( FileEOF( (short)( IFILEHANDLE ) ) ))  ) ) 
                            { 
                            __context__.SourceCodeLine = 84;
                            IRESULT = (short) ( ReadInteger( (short)( IFILEHANDLE ) , ref ILEVEL ) ) ; 
                            __context__.SourceCodeLine = 86;
                            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( IRESULT > 0 ))  ) ) 
                                { 
                                __context__.SourceCodeLine = 88;
                                LEVEL_OUT [ I]  .Value = (ushort) ( ILEVEL ) ; 
                                __context__.SourceCodeLine = 89;
                                I = (ushort) ( (I + 1) ) ; 
                                __context__.SourceCodeLine = 90;
                                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( I > G_IMAXLEVELS ))  ) ) 
                                    {
                                    __context__.SourceCodeLine = 90;
                                    break ; 
                                    }
                                
                                } 
                            
                            else 
                                { 
                                __context__.SourceCodeLine = 94;
                                break ; 
                                } 
                            
                            __context__.SourceCodeLine = 82;
                            } 
                        
                        __context__.SourceCodeLine = 99;
                        Functions.Pulse ( 50, RECALL_OK ) ; 
                        __context__.SourceCodeLine = 101;
                        IRESULT = (short) ( FileClose( (short)( IFILEHANDLE ) ) ) ; 
                        } 
                    
                    else 
                        { 
                        __context__.SourceCodeLine = 105;
                        Print( "ERROR: FileOpen, error code = {0:d}\r\n", (int)IFILEHANDLE) ; 
                        __context__.SourceCodeLine = 107;
                        Functions.Pulse ( 50, RECALL_ERROR ) ; 
                        } 
                    
                    } 
                
                __context__.SourceCodeLine = 112;
                EndFileOperations ( ) ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object SAVE_OnPush_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            short IRESULT = 0;
            short IFILEHANDLE = 0;
            
            ushort I = 0;
            ushort ILEVEL = 0;
            ushort Z = 0;
            
            FILE_INFO FI;
            FI  = new FILE_INFO();
            FI .PopulateDefaults();
            
            
            __context__.SourceCodeLine = 123;
            StartFileOperations ( ) ; 
            __context__.SourceCodeLine = 125;
            IFILEHANDLE = (short) ( FileOpen( M_SFILENAME ,(ushort) ((1 | 256) | 32768) ) ) ; 
            __context__.SourceCodeLine = 127;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( IFILEHANDLE >= 0 ))  ) ) 
                { 
                __context__.SourceCodeLine = 129;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)G_IMAXLEVELS; 
                int __FN_FORSTEP_VAL__1 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 132;
                    LEVEL_OUT [ I]  .Value = (ushort) ( LEVEL_IN[ I ] .UshortValue ) ; 
                    __context__.SourceCodeLine = 134;
                    Z = (ushort) ( LEVEL_IN[ I ] .UshortValue ) ; 
                    __context__.SourceCodeLine = 135;
                    IRESULT = (short) ( WriteInteger( (short)( IFILEHANDLE ) , (ushort)( Z ) ) ) ; 
                    __context__.SourceCodeLine = 137;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( IRESULT <= 0 ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 139;
                        Print( "ERROR writing file.\r\n") ; 
                        __context__.SourceCodeLine = 140;
                        Functions.Pulse ( 50, SAVE_ERROR ) ; 
                        __context__.SourceCodeLine = 141;
                        break ; 
                        } 
                    
                    __context__.SourceCodeLine = 129;
                    } 
                
                __context__.SourceCodeLine = 145;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( IRESULT > 0 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 147;
                    
                    __context__.SourceCodeLine = 150;
                    Functions.Pulse ( 50, SAVE_OK ) ; 
                    } 
                
                __context__.SourceCodeLine = 153;
                IRESULT = (short) ( FileClose( (short)( IFILEHANDLE ) ) ) ; 
                } 
            
            __context__.SourceCodeLine = 157;
            EndFileOperations ( ) ; 
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
object DELETE_OnPush_2 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 164;
        StartFileOperations ( ) ; 
        __context__.SourceCodeLine = 166;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (FileDelete( M_SFILENAME ) != 0))  ) ) 
            {
            __context__.SourceCodeLine = 167;
            Functions.Pulse ( 50, DELETE_ERROR ) ; 
            }
        
        else 
            {
            __context__.SourceCodeLine = 169;
            Functions.Pulse ( 50, DELETE_OK ) ; 
            }
        
        __context__.SourceCodeLine = 171;
        EndFileOperations ( ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object PATH__DOLLAR___OnChange_3 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 178;
        while ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Right( PATH__DOLLAR__ , (int)( 1 ) ) == "\\"))  ) ) 
            { 
            __context__.SourceCodeLine = 181;
            PATH__DOLLAR__  .UpdateValue ( Functions.Left ( PATH__DOLLAR__ ,  (int) ( (Functions.Length( PATH__DOLLAR__ ) - 1) ) )  ) ; 
            __context__.SourceCodeLine = 178;
            } 
        
        __context__.SourceCodeLine = 184;
        M_SFILENAME  .UpdateValue ( PATH__DOLLAR__ + "\\" + FILENAME__DOLLAR__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object FILENAME__DOLLAR___OnChange_4 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 189;
        M_SFILENAME  .UpdateValue ( PATH__DOLLAR__ + "\\" + FILENAME__DOLLAR__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

public override object FunctionMain (  object __obj__ ) 
    { 
    ushort I = 0;
    
    try
    {
        SplusExecutionContext __context__ = SplusFunctionMainStartCode();
        
        __context__.SourceCodeLine = 196;
        G_IMAXLEVELS = (ushort) ( 1 ) ; 
        __context__.SourceCodeLine = 198;
        ushort __FN_FORSTART_VAL__1 = (ushort) ( 200 ) ;
        ushort __FN_FOREND_VAL__1 = (ushort)1; 
        int __FN_FORSTEP_VAL__1 = (int)Functions.ToLongInteger( -( 1 ) ); 
        for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
            { 
            __context__.SourceCodeLine = 201;
            if ( Functions.TestForTrue  ( ( IsSignalDefined( LEVEL_IN[ I ] ))  ) ) 
                { 
                __context__.SourceCodeLine = 203;
                G_IMAXLEVELS = (ushort) ( I ) ; 
                __context__.SourceCodeLine = 204;
                break ; 
                } 
            
            __context__.SourceCodeLine = 198;
            } 
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    return __obj__;
    }
    

public override void LogosSplusInitialize()
{
    _SplusNVRAM = new SplusNVRAM( this );
    M_SFILENAME  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 96, this );
    
    RECALL = new Crestron.Logos.SplusObjects.DigitalInput( RECALL__DigitalInput__, this );
    m_DigitalInputList.Add( RECALL__DigitalInput__, RECALL );
    
    SAVE = new Crestron.Logos.SplusObjects.DigitalInput( SAVE__DigitalInput__, this );
    m_DigitalInputList.Add( SAVE__DigitalInput__, SAVE );
    
    DELETE = new Crestron.Logos.SplusObjects.DigitalInput( DELETE__DigitalInput__, this );
    m_DigitalInputList.Add( DELETE__DigitalInput__, DELETE );
    
    RECALL_OK = new Crestron.Logos.SplusObjects.DigitalOutput( RECALL_OK__DigitalOutput__, this );
    m_DigitalOutputList.Add( RECALL_OK__DigitalOutput__, RECALL_OK );
    
    RECALL_ERROR = new Crestron.Logos.SplusObjects.DigitalOutput( RECALL_ERROR__DigitalOutput__, this );
    m_DigitalOutputList.Add( RECALL_ERROR__DigitalOutput__, RECALL_ERROR );
    
    SAVE_OK = new Crestron.Logos.SplusObjects.DigitalOutput( SAVE_OK__DigitalOutput__, this );
    m_DigitalOutputList.Add( SAVE_OK__DigitalOutput__, SAVE_OK );
    
    SAVE_ERROR = new Crestron.Logos.SplusObjects.DigitalOutput( SAVE_ERROR__DigitalOutput__, this );
    m_DigitalOutputList.Add( SAVE_ERROR__DigitalOutput__, SAVE_ERROR );
    
    DELETE_OK = new Crestron.Logos.SplusObjects.DigitalOutput( DELETE_OK__DigitalOutput__, this );
    m_DigitalOutputList.Add( DELETE_OK__DigitalOutput__, DELETE_OK );
    
    DELETE_ERROR = new Crestron.Logos.SplusObjects.DigitalOutput( DELETE_ERROR__DigitalOutput__, this );
    m_DigitalOutputList.Add( DELETE_ERROR__DigitalOutput__, DELETE_ERROR );
    
    LEVEL_IN = new InOutArray<AnalogInput>( 200, this );
    for( uint i = 0; i < 200; i++ )
    {
        LEVEL_IN[i+1] = new Crestron.Logos.SplusObjects.AnalogInput( LEVEL_IN__AnalogSerialInput__ + i, LEVEL_IN__AnalogSerialInput__, this );
        m_AnalogInputList.Add( LEVEL_IN__AnalogSerialInput__ + i, LEVEL_IN[i+1] );
    }
    
    LEVEL_OUT = new InOutArray<AnalogOutput>( 200, this );
    for( uint i = 0; i < 200; i++ )
    {
        LEVEL_OUT[i+1] = new Crestron.Logos.SplusObjects.AnalogOutput( LEVEL_OUT__AnalogSerialOutput__ + i, this );
        m_AnalogOutputList.Add( LEVEL_OUT__AnalogSerialOutput__ + i, LEVEL_OUT[i+1] );
    }
    
    PATH__DOLLAR__ = new Crestron.Logos.SplusObjects.StringInput( PATH__DOLLAR____AnalogSerialInput__, 64, this );
    m_StringInputList.Add( PATH__DOLLAR____AnalogSerialInput__, PATH__DOLLAR__ );
    
    FILENAME__DOLLAR__ = new Crestron.Logos.SplusObjects.StringInput( FILENAME__DOLLAR____AnalogSerialInput__, 32, this );
    m_StringInputList.Add( FILENAME__DOLLAR____AnalogSerialInput__, FILENAME__DOLLAR__ );
    
    
    RECALL.OnDigitalPush.Add( new InputChangeHandlerWrapper( RECALL_OnPush_0, false ) );
    SAVE.OnDigitalPush.Add( new InputChangeHandlerWrapper( SAVE_OnPush_1, false ) );
    DELETE.OnDigitalPush.Add( new InputChangeHandlerWrapper( DELETE_OnPush_2, false ) );
    PATH__DOLLAR__.OnSerialChange.Add( new InputChangeHandlerWrapper( PATH__DOLLAR___OnChange_3, false ) );
    FILENAME__DOLLAR__.OnSerialChange.Add( new InputChangeHandlerWrapper( FILENAME__DOLLAR___OnChange_4, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public CrestronModuleClass_DYNAMIC_PRESET_V1_1_0 ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint RECALL__DigitalInput__ = 0;
const uint SAVE__DigitalInput__ = 1;
const uint DELETE__DigitalInput__ = 2;
const uint PATH__DOLLAR____AnalogSerialInput__ = 0;
const uint FILENAME__DOLLAR____AnalogSerialInput__ = 1;
const uint LEVEL_IN__AnalogSerialInput__ = 2;
const uint RECALL_OK__DigitalOutput__ = 0;
const uint RECALL_ERROR__DigitalOutput__ = 1;
const uint SAVE_OK__DigitalOutput__ = 2;
const uint SAVE_ERROR__DigitalOutput__ = 3;
const uint DELETE_OK__DigitalOutput__ = 4;
const uint DELETE_ERROR__DigitalOutput__ = 5;
const uint LEVEL_OUT__AnalogSerialOutput__ = 0;

[SplusStructAttribute(-1, true, false)]
public class SplusNVRAM : SplusStructureBase
{

    public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
    
    
}

SplusNVRAM _SplusNVRAM = null;

public class __CEvent__ : CEvent
{
    public __CEvent__() {}
    public void Close() { base.Close(); }
    public int Reset() { return base.Reset() ? 1 : 0; }
    public int Set() { return base.Set() ? 1 : 0; }
    public int Wait( int timeOutInMs ) { return base.Wait( timeOutInMs ) ? 1 : 0; }
}
public class __CMutex__ : CMutex
{
    public __CMutex__() {}
    public void Close() { base.Close(); }
    public void ReleaseMutex() { base.ReleaseMutex(); }
    public int WaitForMutex() { return base.WaitForMutex() ? 1 : 0; }
}
 public int IsNull( object obj ){ return (obj == null) ? 1 : 0; }
}


}
