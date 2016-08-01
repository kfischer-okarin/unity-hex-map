Hex Map Engine
==============

## Description

![Screenshot](screenshot.png?raw=true)

Basic Hex Map data structure, renderer component and generator template.

### How To Use

You need three elements to use this engine: The HexMapRenderer component, a HexTileset Asset and a HexMapGenerator subclass.

* The HexTileset Asset can be created via the Create context menu in Unity's Project tab. You need to set the texture containing the tileset and the tileset dimensions.

* A HexMapGenerator provides the HexMapRenderer with a map to render. Just extend the base HexMapGenerator generator class and implement the GenerateMap method.

* The HexMapRenderer (and the Generator) can just be added to a GameObject. A HexTileset must be assigned to the renderer.

See the source code and comments for more details. There is also a sample scene included.


## References

* Based on the amazing [Hexagonal Grids Guide](http://www.redblobgames.com/grids/hexagons/) from Red Blob Games

## License

The whole source code is releases under the [MIT License](https://opensource.org/licenses/MIT).

The sample tileset was created using the [Hexagon Pack](http://opengameart.org/content/hexagon-pack-310x) by Kenney on OpenGameArt.