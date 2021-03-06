﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 100f;

    [SerializeField] private Transform levelStart;
    [SerializeField] private Transform levelPart_1;
    [SerializeField] private Character character;
    private Vector3 lastEndPosition;

    private void Awake()
    {
        lastEndPosition = levelStart.Find("EndPosition").position;

        //int startingSpawnLevelParts = 5;
        //for(int i=0; i < startingSpawnLevelParts; i++) 
        //{
        //    SpawnLevelPart();
        //}
    }

    private void Update()
    {
        if(Vector3.Distance(character.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART) 
        {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart() 
    {
        Transform lastLevelPartTransform = SpawnLevelPart(lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnLevelPart(Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart_1, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
