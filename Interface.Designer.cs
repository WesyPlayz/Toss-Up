using System.ComponentModel;

namespace Toss_Up;

/// <summary>
/// 
/// </summary>
partial class Interface 
{
    #region PRIVATE   INSTANCE FIELDS

    /// <summary>
    /// 
    /// </summary>
    private IContainer Components = null;

    private Label Prompt_Text;
    private Button Toss_Button;

    #endregion
    #region PROTECTED OVERRIDE FUNCTIONS

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "disposing"></param>
    protected override void Dispose ( bool disposing ) 
    {
        if ( disposing && ( this.Components != null ) ) this.Components.Dispose();

        base.Dispose( disposing );
    }

    #endregion
    #region PRIVATE   INSTANCE FUNCTIONS

    /// <summary>
    /// 
    /// </summary>
    private void Render () 
    {
        this.SuspendLayout();

        this.Components = new Container();

        this.Name = "Toss-Up";
        this.Text = "Toss-Up";

        this.ClientSize = new ( SIZE.X, SIZE.Y );

        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;

        this.BackColor = Color.White;

        this.Prompt_Text = new ()
        {
            Text = PROMPT_TEXT,

            Location = new ( 100, 30 ),
            Size = new ( 600, 60 ),

            TextAlign = ContentAlignment.MiddleCenter
        };
        this.Controls.Add( this.Prompt_Text );
        
        this.Toss_Button = new ()
        {
            Text = BUTTON_TEXT,

            Location = new ( START.X, START.Y ),
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

    /// <summary>
    /// 
    /// </summary>
    private void Display_Prompt () 
    {
        this.Prompt_Text.Text = PROMPT_TEXT;
        this.Toss_Button.Text = BUTTON_TEXT;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "score"></param>
    private void Display_Score ( int score ) 
    {
        this.Prompt_Text.Text = ( score <= this.Best_Score ? $"You scored { score } points!" : $"New best score { score } points!" );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "power"></param>
    private void Display_Power ( int power ) 
    {
        this.Toss_Button.Text = $"{ power }";
    }

    #endregion
}
