import 'package:reptirealm/models/feed.dart';
import 'package:reptirealm/models/morph.dart';
import 'package:reptirealm/models/shed.dart';
import 'package:reptirealm/models/species.dart';
import 'package:reptirealm/models/weight.dart';
import 'package:reptirealm/models/defecation.dart';


class Reptile {

  // REPTILE INFORMATION
  final String? id;
  final String name;
  final String? sex;
  final DateTime? dob;
  final Species? species;
  final List<Morph>? morphs;

  // REPTILE DATA
  final List<Feed>? feeds;
  final List<Shed>? sheds;
  final List<Defecation>? defecations;
  final List<Weight>? weights;

  Reptile({
    this.id,
    required this.name,
    this.sex,
    this.dob,
    this.species,
    this.morphs,

    this.feeds,
    this.sheds,
    this.defecations,
    this.weights,
  });
}
