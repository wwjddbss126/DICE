   M S B A M L   `   `   `  ����F  BDICE.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>.clr-namespace:DICE.Common;assembly=DICE.CommonDICE.Common  5       DICE.Modules'   !DICE.Modules.Views.Cloud.MegaView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aF 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Fdxt<http://schemas.devexpress.com/winfx/2008/xaml/core/themekeys  5      ] YDevExpress.Mvvm.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5      ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      j fDevExpress.Xpf.LayoutControl.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  5	      f	 bDevExpress.Xpf.Accordion.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxa7http://schemas.devexpress.com/winfx/2008/xaml/accordion 	 5
      ;common.clr-namespace:DICE.Common;assembly=DICE.Common 	 5        � 	Resources  5      ?  9DevExpress.Xpf.Utils.Themes.DXTabControlThemeKeyExtension�  �DevExpress.Xpf.Utils.Themes.ThemeKeyExtensionInternalBase`1[[DevExpress.Xpf.Utils.Themes.DXTabControlThemeKeys, DevExpress.Xpf.Core.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]]   ResourceKey�  �DevExpress.Xpf.Utils.Themes.ThemeKeyExtensionBase`1[[DevExpress.Xpf.Utils.Themes.DXTabControlThemeKeys, DevExpress.Xpf.Core.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]]   IsThemeIndependent0  *DevExpress.Mvvm.UI.ObjectToObjectConverter   accordionWidthConverter   DefaultTarget
   Map   DevExpress.Mvvm.UI.MapItem   Source   Target��%�   (        5      $ 
Background=� . )&	  G     �� 6
   .R�5      �� 
������ 
������ $��0q�6H   5        5   
   $ 46��6I   . 5        $
 True��6   $	 242��6+   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $	��850 �6    � ColumnDefinitions 5   
   �� 5      $	��250 �6   �� 5      $
��450* �6   �� 5       .+�5#   
   0  *DevExpress.Xpf.LayoutControl.LayoutControl  $��0��6   .+�5$      / 	 )DevExpress.Xpf.Accordion.AccordionControl  -   5        	  Name$	 	accordion��5$   #   $	��Top=�66   $	��800��6N      DICE.Common.Regions
   MegaCloudNavigation1	  +DevExpress.Mvvm.UI.ModuleInjection.UIRegion 	  Region# Z
 5%      $	��230��5&         AllowHorizontalSizing .     IsCollapseButtonVisible .     AutoExpandAllItems .    ChildrenPath$ Content��5*         
ShowHeader$
 TrueX�5+         HeaderContent$ 
MEGA Cloud��5,         HeaderGlyph:
  4DevExpress.Xpf.Core.Internal.SvgImageSourceConverter$  ../Assets/storage_mega.png
 5-         HeaderGlyphSize$ 32,30X�5.         ItemContainerStyle 50      �� 51       �� 
TargetType, 	 &DevExpress.Xpf.Accordion.AccordionItem"  6    �� Triggers 52      P� 53       P� Property �@ 	ItemLevel P� Value$ 0��6;   . �54   "   ��  �� Property � �� �� Value$ Explorer��6;   �� 55   "    �@ 	GlyphSize$ 33,26X�6>   57      P� 58       �@ 	ItemLevel$ 1��6;   . �59   "   ��  �@ ItemDisplayMode$ Header=�6D   5:      5<      5>      5?        5B   
   $��1��6   $��0��6-   $��2��6:   $��2��6K   .+�5C      *  $DevExpress.Xpf.Core.LoadingDecorator  $��Stretch=�6"   $
��Auto=�6>      LoadingChild. 5D      &   DevExpress.Xpf.Core.DXTabControl     AllowMerging$
 TrueX�6"      TabContentCacheMode$ CacheAllTabs=�66   $��-25,-25, 0, 0q�6Y   $
��Left=�6p      MegaCloudTree# Z 5E   "      View 5F      .  (DevExpress.Xpf.Core.TabControlScrollView  5G      ,  &DevExpress.Xpf.Core.TabControlViewBase    HeaderLocation$
  None=�62   5I      5J      5K      5L      5M      