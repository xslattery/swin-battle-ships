/// <summary>
/// The AIPlayer is a type of player. It can readomly deploy ships, it also has the
/// functionality to generate coordinates and shoot at tiles
/// </summary>
public abstract class AIPlayer : Player
{
    /// <summary>
    /// Location can store the location of the last hit made by an
    /// AI Player. The use of which determines the difficulty.
    /// </summary>
    protected class Location
    {
        private int _Row;
        private int _Column;

        /// <summary>
        /// The row of the shot
        /// </summary>
        /// <value>The row of the shot</value>
        /// <returns>The row of the shot</returns>
        public int Row
        {
            get {
                return _Row;
            }
            set {
                _Row = value;
            }
        }

        /// <summary>
        /// The column of the shot
        /// </summary>
        /// <value>The column of the shot</value>
        /// <returns>The column of the shot</returns>
        public int Column {
            get {
                return _Column;
            }
            set {
                _Column = value;
            }
        }

        /// <summary>
        /// Sets the last hit made to the local variables
        /// </summary>
        /// <param name="row">the row of the location</param>
        /// <param name="column">the column of the location</param>
        public Location (int row, int column)
        {
            _Column = column;
            _Row = row;
        }

        /// <summary>
        /// Check if two locations are equal
        /// </summary>
        /// <param name="this">location 1</param>
        /// <param name="other">location 2</param>
        /// <returns>true if location 1 and location 2 are at the same spot</returns>
        Public Shared Operator =(ByVal this As Location, ByVal other As Location) As Boolean
            Return this IsNot Nothing AndAlso other IsNot Nothing AndAlso this.Row = other.Row AndAlso this.Column = other.Column
        End Operator

        /// <summary>
        /// Check if two locations are not equal
        /// </summary>
        /// <param name="this">location 1</param>
        /// <param name="other">location 2</param>
        /// <returns>true if location 1 and location 2 are not at the same spot</returns>
        Public Shared Operator <>(ByVal this As Location, ByVal other As Location) As Boolean
            Return this Is Nothing OrElse other Is Nothing OrElse this.Row <> other.Row OrElse this.Column <> other.Column
        End Operator
    }


    Public Sub New(ByVal game As BattleShipsGame)
        MyBase.New(game)
    End Sub

    /// <summary>
    /// Generate a valid row, column to shoot at
    /// </summary>
    /// <param name="row">output the row for the next shot</param>
    /// <param name="column">output the column for the next show</param>
    Protected MustOverride Sub GenerateCoords(ByRef row As Integer, ByRef column As Integer)

    /// <summary>
    /// The last shot had the following result. Child classes can use this
    /// to prepare for the next shot.
    /// </summary>
    /// <param name="result">The result of the shot</param>
    /// <param name="row">the row shot</param>
    /// <param name="col">the column shot</param>
    protected mustoverride sub ProcessShot(row as integer, col as integer, result as AttackResult)

    /// <summary>
    /// The AI takes its attacks until its go is over.
    /// </summary>
    /// <returns>The result of the last attack</returns>
    Public Overrides Function Attack() As AttackResult
        Dim result As AttackResult
        Dim row As Integer = 0
        Dim column As Integer = 0

        Do 'keep hitting until a miss
            Delay()

            GenerateCoords(row, column) 'generate coordinates for shot
            result = _game.Shoot(row, column) 'take shot
            ProcessShot(row, column, result)
        Loop While result.Value<> ResultOfAttack.Miss AndAlso result.Value<> ResultOfAttack.GameOver AndAlso Not SwinGame.WindowCloseRequested

        Return result
    End Function

    /// <summary>
    /// Wait a short period to simulate the think time
    /// </summary>
    Private Sub Delay()
        Dim i as Integer
For i  = 0 To 150
            'Dont delay if window is closed
            If SwinGame.WindowCloseRequested Then Return

            SwinGame.Delay(5)
            SwinGame.ProcessEvents()
            SwinGame.RefreshScreen()
        Next
    End Sub

}