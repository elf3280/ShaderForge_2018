using System;

namespace ShaderForge
{
    [Serializable]
    public class SFN_ReflectionProbe:SF_Node
    {
        public SFN_ReflectionProbe()
        {

        }

        public override void Initialize()
        {
            base.Initialize("ReflectionProbe");
            base.showColor = true;
            UseLowerReadonlyValues(true);

            connectors = new SF_NodeConnector[]
            {
                SF_NodeConnector.Create(this,"Out","Out",ConType.cOutput,ValueType.VTv4,false),
                SF_NodeConnector.Create(this,"VR","VR",ConType.cInput,ValueType.VTv3,false),
                SF_NodeConnector.Create(this,"Mip","Mip",ConType.cInput,ValueType.VTv1,false )
            };
        }

        public override string Evaluate(OutChannel channel = OutChannel.All)
        {
             return string.Format(
                    "DecodeHDR(UNITY_SAMPLE_TEXCUBE_LOD(unity_SpecCube0,{0},{1}),unity_SpecCube0_HDR)",
                    GetConnectorByStringID("VR").TryEvaluate(), GetInputIsConnected("Mip")?GetInputCon("Mip").TryEvaluate():0.ToString());
        }
    }
}
