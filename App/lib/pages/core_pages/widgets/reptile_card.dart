import 'package:flutter/material.dart';
import 'package:reptirealm/models/reptile.dart';
import 'package:reptirealm/pages/core_pages/functions_page.dart';


class ReptileCard extends StatelessWidget {
  final Reptile reptile;
  const ReptileCard({super.key, required this.reptile});

  @override
  Widget build(BuildContext context) {

    // NAVIGATION TO ADD NEW REPTILE PAGE
    return GestureDetector(
      onTap: () {
        Navigator.push(
          context,
          PageRouteBuilder(
            pageBuilder: (context, animation, secondaryAnimation) => const ReptileFunctions(),
            transitionDuration: Duration.zero,
            reverseTransitionDuration: Duration.zero,
          ),
        );
      },

      child: Card(
        margin: const EdgeInsets.symmetric(vertical: 4, horizontal: 8), // tighter spacing
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(10)),
        child: Center(
          child: Padding(
            padding: const EdgeInsets.all(12.0), // you can reduce or increase this
            child: Text(
              reptile.name,
              textAlign: TextAlign.center,
              style: const TextStyle(
                fontSize: 24, // adjust size as needed
                fontWeight: FontWeight.bold,
              ),
            ),
          ),
        ),
      ),
    );
  }
}
