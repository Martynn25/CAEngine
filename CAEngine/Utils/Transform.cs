namespace CAEngine.Utils;
using OpenTK.Mathematics;

public class Transform
{
    public Vector3 Position { get; set; } = Vector3.Zero;
        public Vector3 Rotation { get; set; } = Vector3.Zero; // Euler angles (degrees)
        public Vector3 Scale    { get; set; } = Vector3.One;

        public Transform? Parent { get; set; } = null;

        // ------------------------------
        // MATRIX GENERATORS
        // ------------------------------
        
        public Matrix4 GetTranslationMatrix()
        {
            return Matrix4.CreateTranslation(Position);
        }

        public Matrix4 GetScaleMatrix()
        {
            return Matrix4.CreateScale(Scale);
        }

        public Matrix4 GetRotationMatrix()
        {
            // Convert Euler (degrees) → radians → quaternion
            var rotX = MathHelper.DegreesToRadians(Rotation.X);
            var rotY = MathHelper.DegreesToRadians(Rotation.Y);
            var rotZ = MathHelper.DegreesToRadians(Rotation.Z);

            Quaternion q =
                Quaternion.FromEulerAngles(rotX, rotY, rotZ);

            return Matrix4.CreateFromQuaternion(q);
        }

        // ------------------------------
        // LOCAL & WORLD MATRICES
        // ------------------------------

        public Matrix4 LocalMatrix =>
            GetScaleMatrix() *
            GetRotationMatrix() *
            GetTranslationMatrix();

        public Matrix4 WorldMatrix
        {
            get
            {
                if (Parent != null)
                    return LocalMatrix * Parent.WorldMatrix;

                return LocalMatrix;
            }
        }

        public Matrix4 InverseWorldMatrix => WorldMatrix.Inverted();

        // ------------------------------
        // DIRECTION VECTORS
        // ------------------------------

        public Vector3 Forward
        {
            get
            {
                Matrix4 rot = GetRotationMatrix();
                return Vector3.Normalize(new Vector3(rot.M13, rot.M23, rot.M33));
            }
        }

        public Vector3 Right
        {
            get
            {
                Matrix4 rot = GetRotationMatrix();
                return Vector3.Normalize(new Vector3(rot.M11, rot.M21, rot.M31));
            }
        }

        public Vector3 Up
        {
            get
            {
                Matrix4 rot = GetRotationMatrix();
                return Vector3.Normalize(new Vector3(rot.M12, rot.M22, rot.M32));
            }
        }

        // ------------------------------
        // HELPER FUNCTIONS
        // ------------------------------

        public void Translate(Vector3 delta)
        {
            Position += delta;
        }

        public void Rotate(Vector3 deltaDegrees)
        {
            Rotation += deltaDegrees;
        }

        public void SetRotation(Vector3 eulerDegrees)
        {
            Rotation = eulerDegrees;
        }
}