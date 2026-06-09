using System.ComponentModel;

namespace Toss_Up;

/// <summary>
/// 
/// </summary>
partial class Interface 
{
    /// <summary>
    /// 
    /// </summary>
    private IContainer Components = null;

    private Button Toss_Button;

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "disposing"></param>
    protected override void Dispose ( bool disposing ) 
    {
        if ( disposing && ( this.Components != null ) ) this.Components.Dispose();

        base.Dispose( disposing );
    }

    /// <summary>
    /// 
    /// </summary>
    private void Render () 
    {
        this.SuspendLayout();

        this.Components = new Container();

        this.Name = "Toss-Up";
        this.Text = "Toss-Up";

        this.ClientSize = new ( 800, 400 );

        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;

        this.BackColor = Color.White;

        Label text = new ()
        {
            Text = "Hold down the button to toss!",

            Location = new ( 100, 30 ),
            Size = new ( 600, 60 ),

            TextAlign = ContentAlignment.MiddleCenter
        };
        this.Controls.Add( text );
        
        this.Toss_Button = new ()
        {
            Text = "Click Me!",

            Location = new ( 100, 320 ),
            Size = new ( 90, 30 ),

            BackColor = Color.DarkGray
        };
        this.Controls.Add( this.Toss_Button );

        Panel ground = new ()
        {
            Location = new ( 0, 350 ),
            Size = new ( 800, 50 ),

            BackColor = Color.DarkGray
        };
        this.Controls.Add( ground );

        this.ResumeLayout( false );
        this.PerformLayout();

        this.Initialize();
    }
}
