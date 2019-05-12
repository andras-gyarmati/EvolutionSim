using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Creature
    {
        private GameObject _gameObject;
        private List<Knob> knobs;
        private List<Muscle> muscles;
        private string name;

        public float Fitness;
        public float Age;

        private float relativeProbability;
        private float startPos;
        private float endPos;

        float startFitness;
        public float relProb;
        float startPosition;
        float finishPosition;
        float mutationAmount;
        float maxPhysicsValue;
        float minKnobRadius;
        float maxKnobRadius;
        //float displayOffset;

        private Creature(GameObject parent)
        {
            Fitness = 0;
            Age = 0;
            relativeProbability = 0;
            knobs = new List<Knob>();
            muscles = new List<Muscle>();
            _gameObject = new GameObject("Creature");
            _gameObject.transform.SetParent(parent.transform);

            relProb = 0;


            mutationAmount = 0.3f;
            minKnobRadius = 12;
            maxKnobRadius = 17;
        }

        public Creature(GameObject parent, List<Knob> knobs, List<Muscle> muscles) : this(parent)
        {
            this.knobs = null;
            this.muscles = null;
        }

        public Creature(GameObject parent, int knobCount) : this(parent)
        {
            knobs = new List<Knob>();
            muscles = new List<Muscle>();
            for (var i = 0; i < knobCount; i++)
            {
                knobs.Add(new Knob(_gameObject));
            }
            for (var i = 0; i < Controller.RandomFloat(knobCount, (knobCount * (knobCount - 1)) / 2); i++)
            {
                Knob k1 = null;
                Knob k2 = null;
                while (k2 == null)
                {
                    k1 = PickRandomKnobWithLeastConnections();
                    var knob = PickRandomKnobWithLeastConnections();
                    if (k1 != knob && !k1.HasPair(knob))
                    {
                        k2 = knob;
                    }
                }
                muscles.Add(new Muscle(k1, k2, Controller.RandomFloat(1), Controller.RandomFloat(1)));
                k1.AddPair(k2);
                k2.AddPair(k1);
            }
        }

        Knob PickRandomKnobWithLeastConnections()
        {
            var relProbs = new float[knobs.Count];
            Knob knob = null;
            var found = false;
            while (!found)
            {
                float sum = 0;
                foreach (var k in knobs)
                {
                    sum += helperFunction(k.PairCount);
                }
                for (var i = 0; i < knobs.Count; i++)
                {
                    relProbs[i] = helperFunction(knobs[i].PairCount) / sum;
                }
                var picker = Controller.RandomFloat(1);
                var index = 0;
                while (picker > 0 && index < knobs.Count)
                {
                    picker -= relProbs[index];
                    index++;
                }
                index--;
                if (picker < 0)
                {
                    knob = knobs[index];
                    found = true;
                }
            }
            return knob;
        }

        private float helperFunction(float num)
        {
            return Mathf.Pow(2, -num);
        }

        public void Deactivate()
        {
            foreach (Knob knob in knobs)
            {
                knob.Deactivate();
            }
        }

        public void Activate()
        {
            foreach (Knob knob in knobs)
            {
                knob.Activate();
            }
            startFitness = CenterXPos();
        }


        public void Update()
        {
            foreach (Muscle muscle in muscles)
            {
                muscle.Update();
            }
        }


        public Creature NewOffspring(GameObject parent)
        {
            Creature offspring = new Creature(parent, knobs, muscles);
            Mutate(offspring);
            return offspring;
        }

        //TODO knob hozzaadas elvetel, muscle hozzaadas elvetel
        private void Mutate(Creature creature)
        {
            foreach (Knob knob in creature.knobs)
            {
                if (Controller.RandomFloat(1) < Controller.MutationRate)
                {
                    knob.Density = RandomMutationAmount(knob.Density);
                }
                if (Controller.RandomFloat(1) < Controller.MutationRate)
                {
                    knob.Friction = RandomMutationAmount(knob.Friction);
                }
                if (Controller.RandomFloat(1) < Controller.MutationRate)
                {
                    knob.Restitution = RandomMutationAmount(knob.Restitution);
                }
            }
            foreach (Muscle muscle in creature.muscles)
            {
                if (Controller.RandomFloat(1) < Controller.MutationRate)
                {
                    muscle.frequencyHz = RandomMutationAmount(muscle.frequencyHz);
                }
                if (Controller.RandomFloat(1) < Controller.MutationRate)
                {
                    muscle.dampingRatio = RandomMutationAmount(muscle.dampingRatio);
                }
            }
        }

        private float RandomMutationAmount(float currentAmount)
        {
            return currentAmount; //todo fix
        }

        float CalcFitness()
        {
            return CenterXPos() - startFitness;
        }

        private float CenterXPos()
        {
            float sumX = 0;
            foreach (Knob knob in knobs)
            {
                sumX += knob.Pos.x;
            }
            return sumX / knobs.Count;
        }
    }
}