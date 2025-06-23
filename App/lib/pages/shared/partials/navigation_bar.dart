import 'package:flutter/material.dart';
import 'package:reptirealm/pages/core_pages/my_reptiles_page.dart';
import 'package:reptirealm/pages/core_pages/page2.dart';
import 'package:reptirealm/pages/core_pages/page3.dart';
import 'package:reptirealm/pages/core_pages/settings_page.dart';

class NavBar extends StatefulWidget {
  final int? currentIndex;

  const NavBar({super.key, this.currentIndex});

  @override
  State<NavBar> createState() => _NavBarState();
}

class _NavBarState extends State<NavBar> {
  void onItemTapped(int index) {
    if (index == widget.currentIndex) return; // Prevent duplicate navigation

    Widget nextPage;
    switch (index) {
      case 0:
        nextPage = const MyReptiles();
        break;
      case 1:
        nextPage = const Page2();
        break;
      case 2:
        nextPage = const Page3();
        break;
      case 3:
        nextPage = const Settings();
        break;
      default:
        return;
    }

    Navigator.pushReplacement(
      context,
      PageRouteBuilder(
        pageBuilder: (context, animation, secondaryAnimation) => nextPage,
        transitionDuration: Duration.zero,
        reverseTransitionDuration: Duration.zero,
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return BottomNavigationBar(
      currentIndex: widget.currentIndex ?? 0,
      type: BottomNavigationBarType.fixed,
      onTap: onItemTapped,
      selectedItemColor: widget.currentIndex == null ? Colors.grey : null, // Remove highlight
      unselectedItemColor: Colors.grey, // Keep unselected items grey
      items: const [
        BottomNavigationBarItem(
          icon: Icon(Icons.pets),
          label: 'My Reptiles',
        ),
        BottomNavigationBarItem(
          icon: Icon(Icons.notifications),
          label: 'Page 2',
        ),
        BottomNavigationBarItem(
          icon: Icon(Icons.home),
          label: 'Page 3',
        ),
        BottomNavigationBarItem(
          icon: Icon(Icons.settings),
          label: 'Settings',
        ),
      ],
    );
  }
}
