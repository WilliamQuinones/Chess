   M S B A M L   `   `   `  ����@  <Chess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ChessChess  5   	       Chess.Configuration   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Chess     5   	     � Title$  Configuration��5   	   $��357.334��6   $	��454��60    � 	Resources 5	         cmbTemplate 
 P1Rook 
 P1Pawn 
 P2Pawn 
 P2Rook  P2Knight  P2Bishop  P2Queen 
 P2King 	 P1Knight 
 P1Bishop 
 P1King  P1Queen  BackgroundTexture �� ImageSource  P1RookClassy  P1PawnClassy  P2PawnClassy  P2RookClassy  P2KnightClassy  P2BishopClassy  P2QueenClassy  P2KingClassy  P1KnightClassy  P1BishopClassy  P1KingClassy  P1QueenClassy  BackgroundTexture1  P1RookSpook  P1PawnSpook  P2PawnSpook  P2RookSpook  P2KnightSpook   P2BishopSpook ! P2QueenSpook " P2KingSpook # P1KnightSpook $ P1BishopSpook % P1KingSpook & P1QueenSpook ' BackgroundTexture2��%&
  &	        &	 �     &	     &	 ?    &	 m    &	 �    &	 �    &	 �    &	 *    &		 X    &	
 �    &	 �    &	 �    &	     &	 I    &	 ~    &	 �    &	 �    &	     &	 T    &	 �    &	 �    &	 �    &	 -    &	 d    &	 �    &	 �    &	 �    &	 /    &	 b    &	 �    &	 �    &	  �    &	! 2    &	" f    &	# �    &	$ �    &	%     &	& 6    &	' j    �� 5
   
   .R�5      � $��0 0 0 0q�6   $��14��6)   .+�5      �� $��27��6   $��13��6#   $
��Fill=�6/   $��Center=�6W   $��Center=�6r   $��6,0,0,0q�6�   ��6>   �� *Photo+6�   5      5      �� 5   
   $��Resources/p1Rook.bmp��6   �� 5   
   $��Resources/p1Pawn.bmp��6   �� 5   
   $��Resources/p2Pawn.bmp��6   �� 5   
   $��Resources/p2Rook.bmp��6   �� 5   
   $��Resources/p2Knight.bmp��6!   �� 5   
   $��Resources/p2Bishop.bmp��6!   �� 5   
   $��Resources/p2Queen.bmp��6    �� 5   
   $��Resources/p2King.bmp��6   �� 5   
   $��Resources/p1Knight.bmp��6!   �� 5   
   $��Resources/p1Bishop.bmp��6!   �� 5   
   $��Resources/p1King.bmp��6   �� 5   
   $��Resources/p1Queen.bmp��6    �� 5   
   $  Resources/stardust_@2X.png��6/   �� 5   
   $!��Resources/p1RookClassic.png��6%   �� 5   
   $!��Resources/p1PawnClassic.png��6%   �� 5   
   $!��Resources/p2PawnClassic.png��6%   �� 5   
   $!��Resources/p2RookClassic.png��6%   �� 5    
   $#��Resources/p2KnightClassic.png��6'   �� 5!   
   $#��Resources/p2BishopClassic.png��6'   �� 5"   
   $"��Resources/p2QueenClassic.png��6&   �� 5#   
   $!��Resources/p2KingClassic.png��6%   �� 5$   
   $#��Resources/p1KnightClassic.png��6'   �� 5%   
   $#��Resources/p1BishopClassic.png��6'   �� 5&   
   $!��Resources/p1KingClassic.png��6%   �� 5'   
   $"��Resources/p1QueenClassic.png��6&   �� 5(   
   $ Resources/space.jpg��60   �� 5)   
   $��Resources/p1RookSpook.png��6$   �� 5*   
   $��Resources/p1PawnSpook.png��6$   �� 5+   
   $��Resources/p2PawnSpook.png��6$   �� 5,   
   $��Resources/p2RookSpook.png��6$   �� 5-   
   $!��Resources/p2KnightSpook.png��6&   �� 5.   
   $!��Resources/p2BishopSpook.png��6&   �� 5/   
   $ ��Resources/p2QueenSpook.png��6%   �� 50   
   $��Resources/p2KingSpook.png��6$   �� 51   
   $!��Resources/p1KnightSpook.png��6&   �� 52   
   $!��Resources/p1BishopSpook.png��6&   �� 53   
   $��Resources/p1KingSpook.png��6$   �� 54   
   $ ��Resources/p1QueenSpook.png��6%   �� 55   
   $ Resources/wood.jpg��60   56      .��58      �  � ColumnDefinitions 59   
   �� 5:      $	��82* �6   �� 5;      $	��67* �6   ��5=   
   �� 5>      $  Resources/stardust_@2X.png��6   .+�5@   
   �� -   5         ��Name$ button��5@      $��Submit��6!   $
��Left=�62   $��104,282,0,0q�6M   $	��Top=�6b   $��75��6z   $��1��6�   6�   �� 5A   
   -   5         ��Name$ label��5A      $��Board Color��6   $
��Left=�65   $��	10,10,0,0q�6P   $	��Top=�6c   $��Century Gothic)�6{   $��14(�6�   
�������6�   �� 5B   
   -   5        $ 
label_Copy��5B      $��Dark Squares:��6$   $
��Left=�6<   $��	53,37,0,0q�6W   $	��Top=�6j   $��Century Gothic)�6�   $��14(�6�   
�������6�   �� 5C   
   -   5        $ label_Copy1��5C      $��Light Squares:��6%   $
��Left=�6>   $��	53,74,0,0q�6Y   $	��Top=�6l   $��Century Gothic)�6�   $��14(�6�   
�������6�   �� 5D   
   -   5         ��Name$ DarkSquareColorcomboBox��5D      $
��Left=�64   $��
162,42,0,0q�6O   $	��Top=�6c   #��[  6{   $��80��6�   �� 5E   
   -   5        $ LightSquareColorcomboBox��5E      $
��Left=�65   $��
162,79,0,0q�6P   $	��Top=�6d   #��[  6|   $��80��6�   �� 5F   
   -   5        $ label_Copy2��5F      $��Background Theme��6%   $
��Left=�6@   $��
10,113,0,0q�6[   $	��Top=�6o   $��Century Gothic)�6�   $��14(�6�   
�������6�   �� 5G   
   -   5        $ label_Copy3��5G      $��Default��6%   $
��Left=�67   $��
74,142,0,0q�6R   $	��Top=�6f   $��Century Gothic)�6~   $��14(�6�   
�������6�   �� 5H   
   -	   5        $ label_Copy4��5H      $��Classic��6%   $
��Left=�67   $��206,142,0,0q�6R   $	��Top=�6g   $��Century Gothic)�6   $��14(�6�   
�������$��2��6�   �� 5I   
   -
   5        $ label_Copy5��5I      $��Space��6%   $
��Left=�65   $��
92,142,0,0q�6P   $	��Top=�6d   $��Century Gothic)�6|   $��14(�6�   
�������$��1��6�   	� 5J   
   -   5         	�Name$" defaultBackgroundradioButton��5J       	� 	GroupName$ 
Background��6=   $
��Left=�6T   $��
53,150,0,0q�6o   $	��Top=�6�   	 d� 	IsChecked$
	 TrueY�6�   	� 5K   
   -   5        $! classyBackgroundradioButton��5K      $ 
Background��6;   $
��Left=�6R   $��186,150,0,0q�6m   $	��Top=�6�   	� 5L   
   -   5        $  spaceBackgroundradioButton��5L      $ 
Background��6:   $
��Left=�6Q   $��
72,150,0,0q�6l   $	��Top=�6�   $��1��6�   �� 5M   
   -   5        $ label_Copy6��5M      $��Game Piece Theme��6%   $
��Left=�6@   $��
10,183,0,0q�6[   $	��Top=�6o   $��Century Gothic)�6�   $��14(�6�   
�������6�   �� 5N   
   -   5        $ label_Copy7��5N      $��Default��6%   $
��Left=�67   $��
74,212,0,0q�6R   $	��Top=�6f   $��Century Gothic)�6~   $��14(�6�   
�������6�   �� 5O   
   -   5        $ label_Copy8��5O      $��Classic��6%   $
��Left=�67   $��206,212,0,0q�6R   $	��Top=�6g   $��Century Gothic)�6   $��14(�6�   
�������$��2��6�   �� 5P   
   -   5        $ label_Copy9��5P      $��Space��6%   $
��Left=�65   $��
92,212,0,0q�6P   $	��Top=�6d   $��Century Gothic)�6|   $��14(�6�   
�������$��1��6�   	� 5Q   
   -   5        $" defaultPieceThemeradioButton��5Q      $ Pieces��6<   $
��Left=�6O   $��
53,220,0,0q�6j   $	��Top=�6~   $
	 TrueY�6�   	� 5R   
   -   5        $! classyPieceThemeradioButton��5R      $ Pieces��6;   $
��Left=�6N   $��186,220,0,0q�6i   $	��Top=�6~   	� 5S   
   -   5        $! spookyPieceThemeradioButton��5S      $ Pieces��6;   $
��Left=�6N   $��
72,220,0,0q�6i   $	��Top=�6}   $��1��6�   �� 5T   
   -   5        $ button1��5T      $��Cancel��6"   $
��Left=�63   $��
26,282,0,0q�6N   $	��Top=�6b   $��75��6z   6�   5V      5W      