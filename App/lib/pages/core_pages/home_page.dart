import 'package:flutter/material.dart';
import 'package:reptirealm/pages/core_pages/my_reptiles_page.dart';
import 'package:reptirealm/pages/core_pages/page2.dart';
import 'package:reptirealm/pages/core_pages/settings_page.dart';
import '../shared/partials/header_bar.dart';
import '../shared/partials/navigation_bar.dart';
import 'package:reptirealm/pages/core_pages/page3.dart';


class HomePage extends StatefulWidget {
  const HomePage({super.key});

  @override
  State<HomePage> createState() => _HomePageState();
}


class _HomePageState extends State<HomePage> {
  int _selectedIndex = 0;

  // LIST OF PAGES TO NAVIGATE
  final List<Widget> _pages = [
    const MyReptiles(),
    const Page2(),
    const Page3(),
    const Settings(),
  ];

  // FUNCTION TO HANDLE NAVIGATION BETWEEN TABS
  void _onItemTapped(int index) {
    setState(() {
      _selectedIndex = index;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(

      appBar: const HeaderBar(),

      body: _pages[_selectedIndex],

      bottomNavigationBar: NavBar(
        currentIndex: _selectedIndex,
        onItemTapped: _onItemTapped,
      ),
    );
  }
}
