import 'package:flutter/material.dart';


class NavBar extends StatelessWidget {
  final int currentIndex;
  final Function(int) onItemTapped;

  const NavBar({
    super.key,
    required this.currentIndex,
    required this.onItemTapped,
  });

  @override
  Widget build(BuildContext context) {
    return BottomNavigationBar(
      currentIndex: currentIndex,
      type: BottomNavigationBarType.fixed,
      onTap: onItemTapped,
      items: const [

        // MY REPTILES BUTTON
        BottomNavigationBarItem(
          icon: Icon(Icons.pets),
          label: 'My Reptiles',
        ),

        // PAGE 2 BUTTON
        BottomNavigationBarItem(
          icon: Icon(Icons.notifications),
          label: 'Page 2',
        ),

        // PAGE 3 BUTTON
        BottomNavigationBarItem(
          icon: Icon(Icons.home),
          label: 'Page 3',
        ),

        // SETTINGS BUTTON
        BottomNavigationBarItem(
          icon: Icon(Icons.settings),
          label: 'Settings',
        ),
      ],
    );
  }
}
