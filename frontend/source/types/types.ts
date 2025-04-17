export type Species = {
  id?: string;
  modifiedDate?: string;
  name: string;
};

export type Shed = {
  id: string;
  modifiedDate: string;
  date: string;
  blueOrShed: string;
  rating: string;
  comment: string;
};

export type Weight = {
  id: string;
  modifiedDate: string;
  date: string;
  value: number;
  unit: string;
  comment: string;
};

export type Defecation = {
  id: string;
  modifiedDate: string;
  date: string;
  type: string;
  comment: string;
};

export type Feed = {
  id: string;
  modifiedDate: string;
  date: string;
  number: number;
  eaten: boolean;
  comment: string;
  foodType: {
    id: string;
    modifiedDate: string;
    name: string;
  };
};

export type Morph = {
  id?: string;
  modifiedDate?: string;
  name: string;
};

export type AnimalForm = {
  id?: string;
  modifiedDate?: string;
  name: string;
  sex: string;
  dob: string;
  species: Species;
  sheds?: Shed[];
  weights?: Weight[];
  defecations?: Defecation[];
  feeds?: Feed[];
  morphs: Morph[];
};

export type Subscription = {
  id: string;
  modifiedDate: string;
  plan: "free" | "pro";
  status: "active" | "inactive" | "cancelled";
  currentPeriodEnd: string;
  cancelAtPeriodEnd: boolean;
};
