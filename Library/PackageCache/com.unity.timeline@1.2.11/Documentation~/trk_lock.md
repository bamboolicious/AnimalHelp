<!DOCTYPE ParticleEffect SYSTEM '../../../dtd/particle_effect.dtd'>
<ParticleEffect maxParticles="26" space="World" timeToEmit="1" name="ColoringCandyOrangeTrailPFX">
    <Dampening max="1" min="1"/>
    <DirectionalForce x="0" y="0"/>
    <Emitter emissionRate="0.010" spread="360" angle="0">
        <SpawnArea width="12" depth="0" height="12"/>
    </Emitter>
    <EndColor b="255" r="255" g="255" a="255"/>
    <Rotation max="0" min="0"/>
    <RotationSpeed max="0" min="0"/>
    <Size>
        <Max x="29" y="29"/>
        <Min x="10" y="10"/>
    </Size>
    <SizeChange>
        <Max x="-55.640" y="-55.640"/>
        <Min x="-55.640" y="-55.640"/>
    </SizeChange>
    <StartColor b="255" r="255" g="255" a="255"/>
    <Texture numFramesY="1" blend="Multiply" numFramesX="1" file