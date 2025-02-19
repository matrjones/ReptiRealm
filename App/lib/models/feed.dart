import 'package:reptirealm/models/food_types.dart';
import 'package:reptirealm/models/regurgitation.dart';

class Feed {
  final String? id;
  final DateTime date;
  final int number;
  final bool eaten;
  final String? comment;
  final FoodType foodType;
  final Regurgitation? regurgitation;

  Feed({
    this.id,
    required this.date,
    required this.number,
    required this.eaten,
    this.comment,
    required this.foodType,
    this.regurgitation,
  });
}