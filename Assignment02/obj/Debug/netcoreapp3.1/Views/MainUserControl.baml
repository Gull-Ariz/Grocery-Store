   M S B A M L   `   `   `  ����G  CAssignment02, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Assignment02.ViewsAssignment02.Views  5      (    "Assignment02.Views.MainUserControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:Assignment02.Views     5      .��5	      �   � ColumnDefinitions  5
   
   �� 5      $	��100��6   60   �� 5      $��5* �6   6,    � RowDefinitions 5   
   �� 5      $
��0.9* �6   6,   �� 5      $��5* �6   6*   .+�5   
   �� $��Online Grocery Store��6   $��40(�60   $��Center=�6>   $��Center=�6[   $��0��6v   $��1��6�   6�   �� 5   
   	 �� X2$ 1��6   $��0��6   $��Bottom=�6#   $��2��6>    �� Stretch$
 Fill=�6R   
���   �6r   �� 5   
   $��1��6   $��0��6"   $��Center=�62   .+�5      �� $
��Home��6;   $
��Home��6S   $��30��6b   $��20(�6n   $��0,40,0,0q�6|   ��6   �� *updateViewCommand+6�   �� 5      $��Admin��6;   $��30��6T   $��Admin��6`   $��20(�6p   $��0,40,0,0q�6~   ��6   �� *updateViewCommand+6�   �� 5      $��Customer��6;   $��Customer��6W   $��20(�6j   $��30��6x   $��0,40,0,0q�6�   ��6   �� *updateViewCommand+6�   5      �� 5   
   	 �� Y2$ 1��6   $��2��6   $��0��6'   $��Right=�67   $
 Fill=�6S   
���   �6s   �� 5   
   $��1��6   $��1��6'   ��67   �� *SelectedViewModel+6_   5      5      