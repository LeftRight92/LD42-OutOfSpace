using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD42.Scripts.Configuration
{
	public enum ProjectilePhysics
    {
        [PropertyName("cannon")]
        CANNON,
        [PropertyName("beam")]
        BEAM,
        [PropertyName("missile")]
        MISSILE,
        [PropertyName("bomb")]
        BOMB
    }
}