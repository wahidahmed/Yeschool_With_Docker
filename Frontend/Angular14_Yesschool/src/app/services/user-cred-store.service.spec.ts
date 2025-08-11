import { TestBed } from '@angular/core/testing';

import { UserCredStoreService } from './user-cred-store.service';

describe('UserCredStoreService', () => {
  let service: UserCredStoreService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserCredStoreService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
