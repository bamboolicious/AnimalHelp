<!DOCTYPE ParticleEffect SYSTEM '../../../dtd/particle_effect.dtd'>
<ParticleEffect maxParticles="24" space="Local" timeToEmit="0.150" name="ColoringCandyBlueDieSplatterPFX">
    <Dampening max="0.860" min="0.680"/>
    <DirectionalForce x="0" y="0"/>
    <Emitter emissionRate="0" spread="360" angle="0">
        <SpawnArea width="20" depth="0" height="20"/>
    </Emitter>
    <EndColor b="255" r="255" g="255" a="100"/>
    <Rotation max="0" min="0"/>
    <RotationSpeed max="0" min="0"/>
    <Size>
        <Max x="22" y="14"/>
        <Min x="10" y="10"/>
    </Size>
    <SizeChange>
        <Max x="-10" y="-10"/>
        <Min x="-10" y="-10"/>
    </SizeChange>
    <StartColor b="255" r="255" g="255" a="255"/>
    <Texture numFramesY="1" blend="Multiply" numFramesX="1" filename="shared/bootstrap/particles/tex/coloring_blue_blob64.png"/>
    <TextureFrame numTextureAnimationFrames="1" textur