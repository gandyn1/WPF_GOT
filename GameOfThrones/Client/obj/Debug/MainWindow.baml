   M S B A M L   `   `   `  ����X  TXceed.Wpf.Toolkit, Version=2.7.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4P:clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.ToolkitXceed.Wpf.Toolkit  5   	   O KGameOfThrones.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:GameOfThrones.ClientGameOfThrones.Client 5   	   %   GameOfThrones.Client.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   NwpfTool:clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.Toolkit     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:GameOfThrones.Client     5   	   -   5          � Title$  Game Of Thrones��5	   	   $��500.477��6S   $��800.834��6d    � 	Resources 5         StyleControl �� 
TargetType��%+   &	        �� 5   
   " ��6%   5      5      .��5      � -   5         �Name$ gvMain��5       � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6    � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��4* �6   .+�5   
   �� $��1��6   $	��Top=�6$   $��1��6<   $��5��6P   $��Stretch=�6[   �� 5   
   $��2��6   $��5��6(   $��Stretch=�62   � 5    
   $��2��6   $��1��6    ��5!      �� 5"       �� EndPoint$ 0.5,1&�6&    � MappingMode$ RelativeToBoundingBox=�67    �� 
StartPoint$ 0.5,0&�6[   .��5#      � 	 � Color$	 Black��6#   
 � Offset$
 0.083Q�61   � 5$      $	 White��6#   $	
 0.3Q�61   � 5%      $	 	#FF020202��6#   $
 0.917Q�65   � 5&      $	 	#FFF2F3F9��6#   $
 0.693Q�65   5'      .+�5)      (� .��5*      �� -   5         ��Name$ ucBoard��5*      $	��792��6*   $
��1190��67    J� 	AllowDrop . ��5+      �� 5,       �� ImageSource$ Images/GOT_Board.jpg��6%   5.      5/      50      � 52   
   -   5        $ gvChat��52      $��0,5,5,0q�6   $��1��60   $��0��6=    53      �� 54      $��3* �6#   �� 55      $��1* �6#    57      �� 58      $��5* �6    �� 59      $��30 �6    .+�5<      �� #��[  6   $��0��6<   $��2��6I    �� TextWrapping$
 Wrap=�6{    �� AcceptsReturn . {�.  ��6]   �� *ReceivedText+6�   ]� 5=      $��1��6   $��2��6%   ��. .+�5>      �� -   5         ��Name$ 	btnSubmit��5>      $
��Send��6,   $��Right=�6;   $	��100��6R   ��6a   �� *SendMessageCommand+�� 5?      -   5         ��Name$ txtEnteredText��5?      #��[  63   $
��Left=�6Y   $��Stretch=�6�   ��6o   �� *EnteredText+6�   5@      5A      �� 5C   
   $��0��6   $��0��6#   .+�5D      � -   5        $��gvPlayerInfo��5D      $��10,10,10,10q�6'    5E      �� 5F      $��3* �6'   �� 5G      $��1* �6'   .+�5I      �� -   5        $ ctlPlayerName��5I      $��5,0,5,0q�61   6B   #   Xceed.Wpf.Toolkit.ColorPicker  5J      -	   5          Name$ ctlPlayerColor��5J   6   $��1��6&   $��5,0,5,0q�6N   6_   5K      � 5L      -
   5         �Name$ 	ucToolbox��5L      5N      5O      5Q      5S      