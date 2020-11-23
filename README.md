# GameDevRef

## Unity
- [Unlocking Unity Animator's interruption full power](https://www.gamasutra.com/blogs/ArielMarceloMadril/20191003/351550/Unlocking_Unity_Animators_interruption_full_power.php)

## Assets
- [Unity Asset Management (Part 1)](https://starloopstudios.com/unity-asset-management/)

## Shaders
- [Shaders Laboratory](http://www.shaderslab.com/)
- [The Book of Shaders](https://thebookofshaders.com/)
- [A gentle introduction to shaders in Unity3D](http://www.alanzucconi.com/2015/06/10/a-gentle-introduction-to-shaders-in-unity3d/)
- [GLSL Programming/Unity](https://en.wikibooks.org/wiki/GLSL_Programming/Unity)
- [Fun with Shaders and the Depth Buffer](https://chrismflynn.wordpress.com/2012/09/06/fun-with-shaders-and-the-depth-buffer/)
- [C# and Shader Tutorials](https://catlikecoding.com/unity/tutorials/)
- [Shader Coding in Unity from A to Z](https://medium.com/shader-coding-in-unity-from-a-to-z)
- [CgTutorial](http://developer.download.nvidia.com/CgTutorial/cg_tutorial_chapter01.html)
- [Cg Programming/Unity](https://en.wikibooks.org/wiki/Cg_Programming/Unity)
- [Cg Programming](https://en.wikibooks.org/wiki/Cg_Programming)
-- [Vertex Transformations](https://en.wikibooks.org/wiki/Cg_Programming/Vertex_Transformations)
- [Basic Rendering in OpenGL](https://www.informit.com/articles/article.aspx?p=1616796&seqNum=5)
- [x] [Режимы смешивания в Unity](https://habr.com/ru/post/256439/)

#### Blogs
- [humus.name](http://www.humus.name/)

### Youtube Shaders
- [Shaders 101](https://www.youtube.com/watch?v=T-HXmQAMhG0)
- [2D Sprite Shape offical tutorial](https://docs.unity3d.com/Packages/com.unity.2d.spriteshape@1.0/manual/index.html)
- [Working with SpriteShape - Unity tutorial](https://learn.unity.com/tutorial/working-with-spriteshape#)

### Shader solutions
- [FXAA Smoothing Pixels](https://catlikecoding.com/unity/tutorials/advanced-rendering/fxaa/)

## Meshes
- [Runtime Mesh Manipulation With Unity](https://www.raywenderlich.com/3169311-runtime-mesh-manipulation-with-unity)

## Textures
- [Pixel Perfect (forum)](https://forum.unity.com/threads/the-best-pixel-perfect-method.509323/)
- [amplify plugins](http://wiki.amplify.pt/)

## Algorithms 

-[Introduction to Behavior Trees](https://web.archive.org/web/20140723035304/http://www.altdev.co/2011/02/24/introduction-to-behavior-trees/)
-

## Unity Editor
- [ObjectPreview (example)](/UnityEditor/SpriteSizePreview.cs)

## OPEN GL
- [Learn OpenGL](https://learnopengl.com/)

## DirectX
- [DirectX 12 Docs](https://docs.microsoft.com/en-us/windows/win32/direct3d12/directx-12-getting-started)
- [d3dcoder.net/resources](http://www.d3dcoder.net/resources.htm) (source code for the book)

## Matrixes
### Rotation without tanslation
Quick overview of the included variables:
unity_ObjectToWorld - Transforms the mesh vertices from their local mesh space to Unity world space. This is the same world space as your scene. Fairly straightforward.
unity_WorldToObject - Transforms world space into local mesh space.
One thing that is important to understand here is this is not necessarily local gameObject space, this is local mesh space, and these can be different. Most commonly when your model's import settings have a scale factor. If you're using skeletal meshes it's the local position after being transformed by skeletal animation.

Then there's the other UNITY_MATRIX_* values:
UNITY_MATRIX_MVP - This is the most important one, this single matrix does all the transforms from the initial mesh space position into projection space (aka clip space). This matrix is the product of UNITY_MATRIX_M, UNITY_MATRIX_V, and UNITY_MATRIX_P together.
UNITY_MATRIX_M - This is identical to unity_ObjectToWorld, also called the Model transform.
UNITY_MATRIX_V - This is the transform from world space to local View space. This is similar to if you had an gameObject as a child of a camera gameObject, but without any scale applied. This means the positions are all in world space distances, but with the camera at 0,0,0 and rotated to match the camera's orientation.
UNITY_MATRIX_P - This is the transform from view space to Projection space. Projection space, or clip space, can be thought of as the position on screen, with anything on the far left edge of the screen, regardless of how far away, has an x of "-1", and on the right "1". It's actually going to be negative and positive "w", but we'll skip that for now.

There also exist combinations of these 3 main matrices, likes UNITY_MATRIX_MV, or UNITY_MATRIX_VP, which do exactly what you might expect. UNITY_MATRIX_MV transforms from model into view space, and UNITY_MATRIX_VP from world space into projection space.

There are also UNITY_MATRIX_T_MV, UNITY_MATRIX_IT_MV, UNITY_MATRIX_I_V and many other matrices that don't have the UNITY_MATRIX_* prefix, some of which are duplicates (like unity_ObjectToWorld and UNITY_MATRIX_M). We'll ignore those for now.

So that breaks down the different matrices that are available, but doesn't answer your question. For that you need to understand the basics of a transform matrix. A float4x4 transform matrix stores the scale, rotation, and translation with the first 3x3 being the scale and rotation and the last row float3 being the translation. The remaining float4 column we'll ignore as it doesn't really matter for what you're trying to do.

The short version is if you want an object transformed by the rotation and scale but not moved you only want to apply that float3x3 section of the matrix to the vertex positions. There are two main ways to do this:
mul((float3x3)unity_ObjectToWorld, v.vertex.xyz);
or
mul(unity_ObjectToWorld, float4(v.vertex.xyz, 0.0));

These are pretty much identical, though the first method returns a float3 and the second returns a float4.

If you want only rotation and not scale, that's harder and depends on exactly how you want to use the data. Most of the time if you don't care about scale you're dealing with directions, like surface normals. In that case you apply the rotation and scale matrix and normalize the result. Unity has a number of built in functions for this, almost none of which are listed in the documentation. Of you really need rotation with our scale and the vector unnormalized then you have to do a bunch more work.
