using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Proyecto1_01;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1
{
    public class Game : GameWindow
    {
        Escenario escena2 = new Escenario();
        Guion guion = new Guion();

        private Camera camera;
        //-----------------------------------------------------------------------------------------------------------------
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) {}
        //-----------------------------------------------------------------------------------------------------------------
        
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            camera.Update(e);
            base.OnUpdateFrame(e);
        }
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color4.LightCyan);
            //Utilidades.Guardar<Escenario>(escena2, "escenafinal.json");
            escena2 = new Escenario(Utilidades.Cargar<Escenario>("escenafinal.json"));
            CargarCamara();
            SuscribirEventosDeTeclado();
            CargarGuion();
        }
        
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            base.OnUnload(e);
        }
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            //GL.DepthMask(true);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.LoadIdentity();

            Camara();
            Ejes();
            escena2.Dibujar();

            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnResize(EventArgs e)
        {
            float d = 240;//80
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-d, d, -135, 135, -d, d);//16:9
            //GL.Frustum(-80, 80, -80, 80, 4, 100);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            base.OnResize(e);
        }
        private void Ejes()
        {
            // Dibuja los ejes de coordenadas
            GL.LineWidth(2.0f); // Cambia 2.0f al grosor deseado
            GL.Begin(PrimitiveType.Lines);

            // Eje X (rojo)
            GL.Color3(Color.Red); // Rojo
            GL.Vertex3(0.0f, 0.0f, 0.0f); // Origen
            GL.Vertex3(80f, 0.0f, 0.0f); // Punto en X positivo

            // Eje Y (verde)
            GL.Color3(Color.Green); // Verde
            GL.Vertex3(0.0f, 0.0f, 0.0f); // Origen
            GL.Vertex3(0.0f, 80f, 0.0f); // Punto en Y positivo

            // Eje Z (azul)
            GL.Color3(Color.Purple); // Azul
            GL.Vertex3(0.0f, 0.0f, 0.0f); // Origen
            GL.Vertex3(0.0f, 0.0f, 80f); // Punto en Z positivo

            GL.End();
        }
        private void SuscribirEventosDeTeclado()
        {
            var keyboardInputManager = new KeyboardInputManager(escena2, guion);
            keyboardInputManager.OnEscapePressed += () =>
            {
                Exit();
            };
            keyboardInputManager.SubscribeToKeyboardEvents(this);
        }
        private void Camara()
        {
            // Configura la matriz de vista (View Matrix) para cambiar la vista
            Matrix4 viewMatrix = camera.GetViewMatrix();
            // Configura la matriz de vista
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref viewMatrix);
        }
        private void CargarCamara()
        {
            // Configura la cámara con la posición inicial y velocidad
            Vector3 initialPosition = new Vector3(0, 0, 3); // Posición inicial
            Vector3 initialFront = new Vector3(0, 0, -1);  // Dirección hacia donde mira
            Vector3 initialUp = Vector3.UnitY; // Vector "arriba"
            float cameraSpeed = 0.08f; // Velocidad de movimiento
            camera = new Camera(initialPosition, initialFront, initialUp, cameraSpeed);
        }
        private void CargarGuion()
        {
            // Posibles acciones
         
            // Auto

            Punto centroAuto = new Punto(escena2.objetos["auto"].origin);
            Punto centroAutoDelante = new Punto(centroAuto.x + 16, centroAuto.y, centroAuto.z);
            Punto centroAutoAtras = new Punto(centroAuto.x - 16, centroAuto.y, centroAuto.z);

            Action adelante = () => escena2.objetos["auto"].Trasladar(1, 0, 0);
            Action adelante_lento = () => escena2.objetos["auto"].Trasladar(0.5f, 0, 0);
            Action atras = () => escena2.objetos["auto"].Trasladar(-1, 0, 0);
            Action arriba = () => escena2.objetos["auto"].Trasladar(0, 1, 0);
            Action abajo = () => escena2.objetos["auto"].Trasladar(0, -1, 0);

            Action rotar_pos = () => escena2.objetos["auto"].Rotar(-5, 'z', centroAutoDelante);
            Action rotar_neg = () => escena2.objetos["auto"].Rotar(5, 'z', centroAutoAtras);

            // Rotar LLantas en clockwise

            Action llanta1_x = () => escena2.objetos["auto"].partes["llanta1"].Rotar(-5, 'x');
            Action llanta2_x = () => escena2.objetos["auto"].partes["llanta2"].Rotar(-5, 'x');
            Action llanta3_x = () => escena2.objetos["auto"].partes["llanta3"].Rotar(-5, 'x');
            Action llanta4_x = () => escena2.objetos["auto"].partes["llanta4"].Rotar(-5, 'x');

            Action llanta1_y = () => escena2.objetos["auto"].partes["llanta1"].Rotar(-5, 'y');
            Action llanta2_y = () => escena2.objetos["auto"].partes["llanta2"].Rotar(-5, 'y');
            Action llanta3_y = () => escena2.objetos["auto"].partes["llanta3"].Rotar(-5, 'y');
            Action llanta4_y = () => escena2.objetos["auto"].partes["llanta4"].Rotar(-5, 'y');
            
            Action llanta1_z = () => escena2.objetos["auto"].partes["llanta1"].Rotar(-5, 'z');
            Action llanta2_z = () => escena2.objetos["auto"].partes["llanta2"].Rotar(-5, 'z');
            Action llanta3_z = () => escena2.objetos["auto"].partes["llanta3"].Rotar(-5, 'z');
            Action llanta4_z = () => escena2.objetos["auto"].partes["llanta4"].Rotar(-5, 'z');

            //Rotar llantas en counterclockwise

            Action llanta1_x_neg = () => escena2.objetos["auto"].partes["llanta1"].Rotar(5, 'x');
            Action llanta2_x_neg = () => escena2.objetos["auto"].partes["llanta2"].Rotar(5, 'x');
            Action llanta3_x_neg = () => escena2.objetos["auto"].partes["llanta3"].Rotar(5, 'x');
            Action llanta4_x_neg = () => escena2.objetos["auto"].partes["llanta4"].Rotar(5, 'x');

            Action llanta1_y_neg = () => escena2.objetos["auto"].partes["llanta1"].Rotar(5, 'y');
            Action llanta2_y_neg = () => escena2.objetos["auto"].partes["llanta2"].Rotar(5, 'y');
            Action llanta3_y_neg = () => escena2.objetos["auto"].partes["llanta3"].Rotar(5, 'y');
            Action llanta4_y_neg = () => escena2.objetos["auto"].partes["llanta4"].Rotar(5, 'y');

            Action llanta1_z_neg = () => escena2.objetos["auto"].partes["llanta1"].Rotar(5, 'z');
            Action llanta2_z_neg = () => escena2.objetos["auto"].partes["llanta2"].Rotar(5, 'z');
            Action llanta3_z_neg = () => escena2.objetos["auto"].partes["llanta3"].Rotar(5, 'z');
            Action llanta4_z_neg = () => escena2.objetos["auto"].partes["llanta4"].Rotar(5, 'z');

            // LLanta1
            Action llanta1_mover_x = () => escena2.objetos["auto"].partes["llanta1"].Trasladar(1, 0, 0);
            Action llanta1_mover_x_neg = () => escena2.objetos["auto"].partes["llanta1"].Trasladar(-1, 0, 0);

            Action llanta1_mover_z = ()=> escena2.objetos["auto"].partes["llanta1"].Trasladar(0, 0, 1);
            Action llanta1_mover_z_neg = () => escena2.objetos["auto"].partes["llanta1"].Trasladar(0, 0, -1f);

            Action llanta1_mover_y = () => escena2.objetos["auto"].partes["llanta1"].Trasladar(0, 1, 0);
            Action llanta1_mover_y_neg = () => escena2.objetos["auto"].partes["llanta1"].Trasladar(0, -1f, 0);

            // LLanta2
            Action llanta2_mover_x = () => escena2.objetos["auto"].partes["llanta2"].Trasladar(1, 0, 0);
            Action llanta2_mover_x_neg = () => escena2.objetos["auto"].partes["llanta2"].Trasladar(-1, 0, 0);

            Action llanta2_mover_z = () => escena2.objetos["auto"].partes["llanta2"].Trasladar(0, 0, 1);
            Action llanta2_mover_z_neg = () => escena2.objetos["auto"].partes["llanta2"].Trasladar(0, 0, -1f);

            Action llanta2_mover_y = () => escena2.objetos["auto"].partes["llanta2"].Trasladar(0, 1, 0);
            Action llanta2_mover_y_neg = () => escena2.objetos["auto"].partes["llanta2"].Trasladar(0, -1f, 0);

            //Llanta3
            Action llanta3_mover_x = () => escena2.objetos["auto"].partes["llanta3"].Trasladar(1, 0, 0);
            Action llanta3_mover_x_neg = () => escena2.objetos["auto"].partes["llanta3"].Trasladar(-1, 0, 0);

            Action llanta3_mover_z = () => escena2.objetos["auto"].partes["llanta3"].Trasladar(0, 0, 1);
            Action llanta3_mover_z_neg = () => escena2.objetos["auto"].partes["llanta3"].Trasladar(0, 0, -1f);

            Action llanta3_mover_y = () => escena2.objetos["auto"].partes["llanta3"].Trasladar(0, 1, 0);
            Action llanta3_mover_y_neg = () => escena2.objetos["auto"].partes["llanta3"].Trasladar(0, -1f, 0);

            //LLanta4
            Action llanta4_mover_x = () => escena2.objetos["auto"].partes["llanta4"].Trasladar(1, 0, 0);
            Action llanta4_mover_x_neg = () => escena2.objetos["auto"].partes["llanta4"].Trasladar(-1, 0, 0);

            Action llanta4_mover_z = () => escena2.objetos["auto"].partes["llanta4"].Trasladar(0, 0, 1);
            Action llanta4_mover_z_neg = () => escena2.objetos["auto"].partes["llanta4"].Trasladar(0, 0, -1f);

            Action llanta4_mover_y = () => escena2.objetos["auto"].partes["llanta4"].Trasladar(0, 1, 0);
            Action llanta4_mover_y_neg = () => escena2.objetos["auto"].partes["llanta4"].Trasladar(0, -1f, 0);



            //Ir delante
            Accion acto1 = new Accion();
            acto1.objeto.Add(adelante);
            //Caida
            Accion acto2 = new Accion();
            acto2.objeto.Add(rotar_pos);
            acto2.objeto.Add(adelante_lento);
            acto2.objeto.Add(abajo);
            //Rectificacion
            Accion acto3 = new Accion();
            acto3.objeto.Add(rotar_neg);
            //LLantas volando
            Accion acto4 = new Accion();
            acto4.objeto.Add(llanta1_mover_z);
            acto4.objeto.Add(llanta1_mover_y_neg);
            acto4.objeto.Add(llanta1_z_neg);

            acto4.objeto.Add(llanta2_mover_z);
            acto4.objeto.Add(llanta2_mover_y_neg);
            acto4.objeto.Add(llanta2_z_neg);

            acto4.objeto.Add(llanta3_mover_z);
            acto4.objeto.Add(llanta3_mover_y_neg);
            acto4.objeto.Add(llanta3_z_neg);
            
            acto4.objeto.Add(llanta4_mover_z);
            acto4.objeto.Add(llanta4_mover_y_neg);
            acto4.objeto.Add(llanta4_z_neg);

            //LLantas rotando
            Accion acto5 = new Accion();
            acto5.objeto.Add(llanta1_mover_x_neg);
            acto5.objeto.Add(llanta2_mover_x_neg);
            acto5.objeto.Add(llanta3_mover_x_neg);
            acto5.objeto.Add(llanta4_mover_x_neg);

            acto5.objeto.Add(llanta1_mover_z);
            acto5.objeto.Add(llanta3_mover_z);
            acto5.objeto.Add(llanta2_mover_z_neg);
            acto5.objeto.Add(llanta4_mover_z_neg);

            acto5.objeto.Add(llanta1_z_neg);
            acto5.objeto.Add(llanta2_z_neg);
            acto5.objeto.Add(llanta3_z_neg);
            acto5.objeto.Add(llanta4_z_neg);

            //Cargar Actos
            guion.AgregarAccion(acto1.objeto, 50);//Adelante
            guion.AgregarAccion(acto2.objeto, 50);//caida
            guion.AgregarAccion(acto3.objeto, 15);//rectificacion
            guion.AgregarAccion(acto4.objeto, 10);//llantas volando
            guion.AgregarAccion(acto5.objeto, 40);//llantas rotando
            
            //mover las llantas
        }
    }
}
