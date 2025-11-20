using OpenTK.Mathematics;
namespace CAEngine.Rendering;

 public static class Shapes
    {
        public static Mesh CreateRegionedCube(float size = 1.0f)
        {
            float h = size / 2.0f;

            // 3x2 atlas: see layout in the explanation
            const int cols = 3;
            const int rows = 2;

            var top    = UvRegion.FromCell(0, 1, cols, rows); // (0,1)
            var left   = UvRegion.FromCell(1, 1, cols, rows); // (1,1)
            var right  = UvRegion.FromCell(2, 1, cols, rows); // (2,1)
            var bottom = UvRegion.FromCell(0, 0, cols, rows); // (0,0)
            var back   = UvRegion.FromCell(1, 0, cols, rows); // (1,0)
            var front  = UvRegion.FromCell(2, 0, cols, rows); // (2,0)

            // We’ll build 24 vertices (4 per face) with per-face UVs
            var vertices = new[]
            {
                // FRONT face (+Z) uses 'front'
                new Vertex(new Vector3(-h, -h,  h), new Vector2(front.Min.X, front.Min.Y)),
                new Vertex(new Vector3( h, -h,  h), new Vector2(front.Max.X, front.Min.Y)),
                new Vertex(new Vector3( h,  h,  h), new Vector2(front.Max.X, front.Max.Y)),
                new Vertex(new Vector3(-h,  h,  h), new Vector2(front.Min.X, front.Max.Y)),

                // BACK face (-Z) uses 'back'
                new Vertex(new Vector3( h, -h, -h), new Vector2(back.Min.X, back.Min.Y)),
                new Vertex(new Vector3(-h, -h, -h), new Vector2(back.Max.X, back.Min.Y)),
                new Vertex(new Vector3(-h,  h, -h), new Vector2(back.Max.X, back.Max.Y)),
                new Vertex(new Vector3( h,  h, -h), new Vector2(back.Min.X, back.Max.Y)),

                // LEFT face (-X) uses 'left'
                new Vertex(new Vector3(-h, -h, -h), new Vector2(left.Min.X, left.Min.Y)),
                new Vertex(new Vector3(-h, -h,  h), new Vector2(left.Max.X, left.Min.Y)),
                new Vertex(new Vector3(-h,  h,  h), new Vector2(left.Max.X, left.Max.Y)),
                new Vertex(new Vector3(-h,  h, -h), new Vector2(left.Min.X, left.Max.Y)),

                // RIGHT face (+X) uses 'right'
                new Vertex(new Vector3( h, -h,  h), new Vector2(right.Min.X, right.Min.Y)),
                new Vertex(new Vector3( h, -h, -h), new Vector2(right.Max.X, right.Min.Y)),
                new Vertex(new Vector3( h,  h, -h), new Vector2(right.Max.X, right.Max.Y)),
                new Vertex(new Vector3( h,  h,  h), new Vector2(right.Min.X, right.Max.Y)),

                // TOP face (+Y) uses 'top'
                new Vertex(new Vector3(-h,  h,  h), new Vector2(top.Min.X, top.Min.Y)),
                new Vertex(new Vector3( h,  h,  h), new Vector2(top.Max.X, top.Min.Y)),
                new Vertex(new Vector3( h,  h, -h), new Vector2(top.Max.X, top.Max.Y)),
                new Vertex(new Vector3(-h,  h, -h), new Vector2(top.Min.X, top.Max.Y)),

                // BOTTOM face (-Y) uses 'bottom'
                new Vertex(new Vector3(-h, -h, -h), new Vector2(bottom.Min.X, bottom.Min.Y)),
                new Vertex(new Vector3( h, -h, -h), new Vector2(bottom.Max.X, bottom.Min.Y)),
                new Vertex(new Vector3( h, -h,  h), new Vector2(bottom.Max.X, bottom.Max.Y)),
                new Vertex(new Vector3(-h, -h,  h), new Vector2(bottom.Min.X, bottom.Max.Y)),
            };

            var indices = new uint[]
            {
                // Front
                0, 1, 2,  2, 3, 0,
                // Back
                4, 5, 6,  6, 7, 4,
                // Left
                8, 9,10, 10,11, 8,
                // Right
                12,13,14, 14,15,12,
                // Top
                16,17,18, 18,19,16,
                // Bottom
                20,21,22, 22,23,20
            };

            return new Mesh(vertices, indices);
        }
    }