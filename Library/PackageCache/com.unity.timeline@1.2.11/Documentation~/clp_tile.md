<!DOCTYPE ParticleEffect SYSTEM '../../dtd/particle_effect.dtd'>
<ParticleEffect maxParticles="10" space="Local" timeToEmit="0.100" name="ColorBombSwapSparkle">
    <Dampening max="1.200" min="0.800"/>
    <DirectionalForce x="0" y="-1400"/>
    <Emitter emissionRate="0" spread="0" angle="0">
        <SpawnArea width="50" depth="0" height="50"/>
    </Emitter>
    <EndColor b="255" r="255" g="255" a="0"/>
    <Rotation max="0" min="0"/>
    <RotationSpeed max="0" min="0"/>
    <Size>
        <Max x="65" y="122"/>
        <Min x="65" y="122"/>
    </Size>
    <SizeChange>
        <Max x="-32" y="-32"/>
        <Min x="0" y="0"/>
    </SizeChange>
    <StartColor b="255" r="255" g="255" a="100"/>
    <Texture numFramesY="1" blend="Multiply" numFramesX="1" filename="shared/bootstrap/particles/tex/nice_spark.png"/>
    <TextureFrame numTextureAnimationFrames="1" textureAnimationSpeed="50" startFrame="0