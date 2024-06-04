#include <SDL.h>
#include <iostream>
#include "SDL_image.h"

#include "MClassM.h"

extern int windowWidth;
extern int windowHeight;
extern long long int framesCounter;

MClassM::MClassM() {
    window = nullptr;
    renderer = nullptr;
}

void MClassM::Start() {
    SDL_Init(SDL_INIT_EVERYTHING);
    window = SDL_CreateWindow("Window", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, windowWidth, windowHeight, SDL_WINDOW_SHOWN);
    renderer = SDL_CreateRenderer(window, -1, 0);

    SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);
    LoadTextures();

}

void MClassM::LoadTextures() {
    //example->SetTexture(load("textures/example.png", renderer)); Example Texture Load
}

void MClassM::Events() {
    framesCounter++;
}

void MClassM::Exit(bool& status, const Uint8* state) {
    if (state[SDL_SCANCODE_ESCAPE]) {
        status = false;
    }
}

void MClassM::Movement(bool &status) {
    SDL_PumpEvents();
    const Uint8* state = SDL_GetKeyboardState(NULL);
    Exit(status,state);
}

void MClassM::Render() {
    SDL_RenderClear(renderer);
    //SDL_RenderCopy(renderer, textback, NULL, &rectback); Example Direct Rendering
    SDL_RenderPresent(renderer);
}

SDL_Texture* MClassM::load(const char* file, SDL_Renderer* ren) {
    SDL_Surface* tmpSurface = IMG_Load(file);
    SDL_Texture* tex = SDL_CreateTextureFromSurface(ren, tmpSurface);
    SDL_FreeSurface(tmpSurface);
    return tex;
}

MClassM::~MClassM() {
    SDL_DestroyRenderer(renderer);
    SDL_DestroyWindow(window);
    SDL_Quit();
    //std::cout << "Resources Destroyed";
}
