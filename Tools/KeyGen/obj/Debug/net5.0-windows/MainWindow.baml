   M S B A M L   `   `   `  ����A  =KeyGen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:KeyGenKeyGen  5   	       KeyGen.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:KeyGen     5   	     � WindowStartupLocation$  CenterScreen=�5       � WindowStyle$
 None=�5   	    � Title$ KeyGen��5	   	   $	��200��6   $	��300��6%   .��5
      �  � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6    � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   ��5   
   �� 5       �� ImageSource$ ./fractal.jpg��6   .+�5   
   �� -   5        $��20��5      $��20��6   $��1��6(   
������ 
������ $	��Top=�5      $��Right=�6)   $��
0, 0, 5, 0q�5      .��5      �� $��X��6   $
��Bold"�6!   5      � 5   
   $��0��6   $��0��6    5      �� 5       $��1* �6    �� 5!      $��1* �6    .+�5#      �� $��0��6   $��Center=�5$      $��Bottom=�61   .��6L   Taille de votre clé5&      �� 5'      -   5         ��Name$
 size��5'   #   $��1��6   $��Center=�5(      $	��Top=�63   $��20��5)      $	��100��6"   5*      �� 5,   
   -   5        $��0��5,      $��1��6   $��30��5-      $	��100��6   .��6>   Generate Key5/      �� 50   
   -   5        $ 	secretKey��50   3   $��1��6   $��2��6    �� TextWrapping$ WrapWithOverflow=�51       �� TextAlignment$ Center=�64   $��Center=�6K   $	��250��52      $��70��6    $��0, -15, 0, 0q�6,   54      55      