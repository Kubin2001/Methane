#include <iostream>
#include <SDL.h>
#include "MClassM.h"

MClassM::MClassM(SDL_Renderer * renderer) {
    this->renderer = renderer;
}

SDL_Texture* MClassM::GetTexture() {
    return texture;
}

void MClassM::SetTexture(SDL_Texture * temptex) {
    texture = temptex;
}

SDL_Rect* MClassM::GetRectangle() {
    return &rectangle;
}

void MClassM::Render() {
    SDL_RenderCopy(renderer, texture, NULL, &rectangle);
}

MClassM::~MClassM() {
    SDL_DestroyTexture(texture);
}

