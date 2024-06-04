#pragma once
#include <SDL.h>

class MClassM {
private:
    SDL_Window* window;
    SDL_Renderer* renderer;

public:
    MClassM();

    void Start();

    void LoadTextures();

    void Events();

    void Exit(bool& status, const Uint8* state);

    void Movement(bool& status);

    void Render();

    SDL_Texture* load(const char* file, SDL_Renderer* ren);

    ~MClassM();
};
