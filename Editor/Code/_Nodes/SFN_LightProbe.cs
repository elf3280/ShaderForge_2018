using System;


namespace ShaderForge
{
    [System.Serializable]
    public class SFN_LightProbe : SF_Node_Arithmetic
    {
        public SFN_LightProbe()
        {

        }

        public override void Initialize()
        {
            base.Initialize("LightProbe");
            base.PrepareArithmetic(1);
            base.shaderGenMode = ShaderGenerationMode.SimpleFunction;
            UseLowerReadonlyValues(true);

            connectors = new SF_NodeConnector[]
            {
                SF_NodeConnector.Create(this,"Out","",ConType.cOutput,ValueType.VTv3,false),
                SF_NodeConnector.Create(this,"IN","",ConType.cInput,ValueType.VTv3,false).SetRequired(true)
            };
        }

        public override string Evaluate(OutChannel channel = OutChannel.All)
        {
            return String.Format("ShadeSH9(float4({0},1))",GetConnectorByStringID("IN").TryEvaluate());
        }

    }
}
