using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace FlatKit
{
    public class FlatKitOutline : ScriptableRendererFeature
    {
        class OutlinePass : ScriptableRenderPass
        {
            private RenderTargetIdentifier source { get; set; }
            private RenderTargetHandle destination { get; set; }
            public Material outlineMaterial = null;
            RenderTargetHandle temporaryColorTexture;

            public void Setup(RenderTargetIdentifier source, RenderTargetHandle destination)
            {
                this.source = source;
                this.destination = destination;
            }

            public OutlinePass(Material outlineMaterial)
            {
                this.outlineMaterial = outlineMaterial;
            }

            public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
            {
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                CommandBuffer cmd = CommandBufferPool.Get("FlatKit Outline Pass");

                RenderTextureDescriptor opaqueDescriptor = renderingData.cameraData.cameraTargetDescriptor;
                opaqueDescriptor.depthBufferBits = 0;

                if (destination == RenderTargetHandle.CameraTarget)
                {
                    cmd.GetTemporaryRT(temporaryColorTexture.id, opaqueDescriptor, FilterMode.Point);
                    Blit(cmd, source, temporaryColorTexture.Identifier(), outlineMaterial, 0);
                    Blit(cmd, temporaryColorTexture.Identifier(), source);
                }
                else Blit(cmd, source, destination.Identifier(), outlineMaterial, 0);

                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }

            public override void FrameCleanup(CommandBuffer cmd)
            {
                if (destination == RenderTargetHandle.CameraTarget)
                {
                    cmd.ReleaseTemporaryRT(temporaryColorTexture.id);
                }
            }
        }

        [Header("Create > FlatKit > Outline Settings")]
        public OutlineSettings settings;

        private Material _material = null;
        private OutlinePass _outlinePass;
        private RenderTargetHandle _outlineTexture;

        private static readonly string ShaderName = "Hidden/FlatKit/OutlineFilter";
        private static readonly int EdgeColor = Shader.PropertyToID("_EdgeColor");
        private static readonly int Thickness = Shader.PropertyToID("_Thickness");
        private static readonly int DepthThresholdMin = Shader.PropertyToID("_DepthThresholdMin");
        private static readonly int DepthThresholdMax = Shader.PropertyToID("_DepthThresholdMax");
        private static readonly int NormalThresholdMin = Shader.PropertyToID("_NormalThresholdMin");
        private static readonly int NormalThresholdMax = Shader.PropertyToID("_NormalThresholdMax");
        private static readonly int ColorThresholdMin = Shader.PropertyToID("_ColorThresholdMin");
        private static readonly int ColorThresholdMax = Shader.PropertyToID("_ColorThresholdMax");

        public override void Create()
        {
            if (settings == null)
            {
                Debug.LogWarning("[FlatKit] Missing Outline Settings");
                return;
            }

#if UNITY_EDITOR
            ShaderIncludeUtilities.AddAlwaysIncludedShader(ShaderName);
#endif

            InitMaterial();
            
            _outlinePass = new OutlinePass(_material)
            {
                renderPassEvent = RenderPassEvent.AfterRenderingTransparents
            };
            _outlineTexture.Init("_OutlineTexture");
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (settings == null)
            {
                Debug.LogWarning("[FlatKit] Missing Outline Settings");
                return;
            }

            InitMaterial();

            _outlinePass.Setup(renderer.cameraColorTarget, RenderTargetHandle.CameraTarget);
            renderer.EnqueuePass(_outlinePass);
        }

        private void InitMaterial()
        {
            if (_material == null)
            {
                _material = new Material(Shader.Find(ShaderName));
            }

            if (_material == null)
            {
                Debug.LogWarning("[FlatKit] Missing Outline Material");
            }
            
            UpdateShader();
        }

        private void UpdateShader()
        {
            if (_material == null) {
                return;
            }
            
            const string depthKeyword = "OUTLINE_USE_DEPTH";
            if (settings.useDepth) {
                _material.EnableKeyword(depthKeyword);
            }
            else {
                _material.DisableKeyword(depthKeyword);
            }

            const string normalsKeyword = "OUTLINE_USE_NORMALS";
            if (settings.useNormals) {
                _material.EnableKeyword(normalsKeyword);
            }
            else {
                _material.DisableKeyword(normalsKeyword);
            }

            const string colorKeyword = "OUTLINE_USE_COLOR";
            if (settings.useColor) {
                _material.EnableKeyword(colorKeyword);
            }
            else {
                _material.DisableKeyword(colorKeyword);
            }

            const string outliineOnlyKeyword = "OUTLINE_ONLY";
            if (settings.outlineOnly) {
                _material.EnableKeyword(outliineOnlyKeyword);
            }
            else {
                _material.DisableKeyword(outliineOnlyKeyword);
            }

            _material.SetColor(EdgeColor, settings.edgeColor);
            _material.SetFloat(Thickness, settings.thickness);
            
            _material.SetFloat(DepthThresholdMin, settings.minDepthThreshold);
            _material.SetFloat(DepthThresholdMax, settings.maxDepthThreshold);
            
            _material.SetFloat(NormalThresholdMin, settings.minNormalsThreshold);
            _material.SetFloat(NormalThresholdMax, settings.maxNormalsThreshold);
            
            _material.SetFloat(ColorThresholdMin, settings.minColorThreshold);
            _material.SetFloat(ColorThresholdMax, settings.maxColorThreshold);
        }
    }
}