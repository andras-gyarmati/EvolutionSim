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

            var pairCount = Random.Range(knobCount, (knobCount * (knobCount - 1)) / 2);

            for (var i = 0; i < pairCount; i++)
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
                muscles.Add(new Muscle(_gameObject, k1, k2, Random.Range(0, 1f), Random.Range(0, 1f)));
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

                var picker = Random.Range(0, 1f);
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

            Age += Time.fixedDeltaTime;
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
                if (Random.Range(0, 1f) < Controller.MutationRate)
                {
                    knob.Density = RandomMutationAmount(knob.Density);
                }
                if (Random.Range(0, 1f) < Controller.MutationRate)
                {
                    knob.Friction = RandomMutationAmount(knob.Friction);
                }
                if (Random.Range(0, 1f) < Controller.MutationRate)
                {
                    knob.Restitution = RandomMutationAmount(knob.Restitution);
                }
            }
            foreach (Muscle muscle in creature.muscles)
            {
                if (Random.Range(0, 1f) < Controller.MutationRate)
                {
                    muscle.frequencyHz = RandomMutationAmount(muscle.frequencyHz);
                }
                if (Random.Range(0, 1f) < Controller.MutationRate)
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