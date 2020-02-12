<!DOCTYPE ParticleEffect SYSTEM '../../../dtd/particle_effect.dtd'>
<ParticleEffect maxParticles="1" space="Local" timeToEmit="0.100" name="ColoringCandyBlackImpact02PFX">
    <Dampening max="0.400" min="0.700"/>
    <DirectionalForce x="0" y="0"/>
    <Emitter emissionRate="0" spread="360" angle="0">
        <SpawnArea width="10" depth="0" height="10"/>
    </Emitter>
    <EndColor b="255" r="255" g="255" a="0"/>
    <Rotation max="0" min="0"/>
    <RotationSpeed max="0" min="0"/>
    <Size>
        <Max x="100" y="100"/>
        <Min x="60" y="60"/>
    </Size>
    <SizeChange>
        <Max x="-140" y="-140"/>
        <Min x="-50" y="-50"/>
    </SizeChange>
    <StartColor b="255" r="255" g="255" a="255"/>
    <Texture numFramesY="1" blend="Multiply" numFramesX="1" filename="shared/bootstrap/particles/tex/coloring_candy_impact_black_02.png"/>
    <TextureFrame numTextureAnimationFrames="1" textureAnimationSpeed="50" startF