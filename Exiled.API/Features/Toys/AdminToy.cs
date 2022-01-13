﻿// -----------------------------------------------------------------------
// <copyright file="AdminToy.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.API.Features.Toys
{
    using AdminToys;
    using Exiled.API.Enums;
    using Mirror;
    using UnityEngine;

    /// <summary>
    /// A wrapper class for <see cref="AdminToys.AdminToyBase"/>.
    /// </summary>
    public abstract class AdminToy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminToy"/> class.
        /// </summary>
        /// <param name="toyAdminToyBase">The <see cref="AdminToys.AdminToyBase"/> to be wrapped.</param>
        /// <param name="type">The <see cref="AdminToyType"/> of the object.</param>
        public AdminToy(AdminToyBase toyAdminToyBase, AdminToyType type)
        {
            AdminToyBase = toyAdminToyBase;
            ToyType = type;
        }

        /// <summary>
        /// Gets the original <see cref="AdminToys.AdminToyBase"/>.
        /// </summary>
        public AdminToyBase AdminToyBase { get; }

        /// <summary>
        /// Gets the <see cref="AdminToyType"/>.
        /// </summary>
        public AdminToyType ToyType { get; }

        /// <summary>
        /// Gets or sets the position of the toy.
        /// </summary>
        public Vector3 Position
        {
            get => AdminToyBase.transform.position;
            set => AdminToyBase.transform.position = value;
        }

        /// <summary>
        /// Gets or sets the rotation of the toy.
        /// </summary>
        public Quaternion Rotation
        {
            get => AdminToyBase.transform.rotation;
            set => AdminToyBase.transform.rotation = value;
        }

        /// <summary>
        /// Gets or sets the scale of the toy.
        /// </summary>
        public Vector3 Scale
        {
            get => AdminToyBase.transform.localScale;
            set => AdminToyBase.transform.localScale = value;
        }

        /// <summary>
        /// Gets or sets the movement smoothing value of the toy.
        /// <para>
        /// Higher values reflect smoother movements.
        /// <br /> - 60 is an ideal value.
        /// </para>
        /// </summary>
        public byte MovementSmoothing
        {
            get => AdminToyBase.MovementSmoothing;
            set => AdminToyBase.NetworkMovementSmoothing = value;
        }

        /// <summary>
        /// Spawns the light into the game. Use <see cref="UnSpawn"/> to remove it.
        /// </summary>
        public void Spawn() => NetworkServer.Spawn(AdminToyBase.gameObject);

        /// <summary>
        /// Removes the light from the game. Use <see cref="Spawn"/> to bring it back.
        /// </summary>
        public void UnSpawn() => NetworkServer.UnSpawn(AdminToyBase.gameObject);
    }
}
